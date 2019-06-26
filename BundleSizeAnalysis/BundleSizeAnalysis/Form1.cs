using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BundleSizeAnalysis
{
    public enum ReadState
    {
        NONE,
        BEGIN,
        BUNDLE,
        END,
    }

    public partial class Form1 : Form
    {
        private ReadState readState = ReadState.NONE;
        private List<BundleInfo> bundles = new List<BundleInfo>();
        private ValueType valueType;

        public Form1()
        {
            InitializeComponent();
            InitListView(BundleList);
            InitAssetListView();
            string file = string.Format(@"C:\Users\{0}\AppData\Local\Unity\Editor\Editor.log", Environment.UserName);
            ParseFile(file);
        }

        private void btn_select_file_Click(object sender, EventArgs e)
        {
            if(OpenFile.CheckFileExists)
                OpenFile.InitialDirectory = Directory.GetParent(OpenFile.FileName).FullName;
            else
                OpenFile.InitialDirectory = string.Format(@"C:\Users\{0}\AppData\Local\Unity\Editor", Environment.UserName); 
            OpenFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OpenFile.FilterIndex = 2;
            OpenFile.RestoreDirectory = true;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                ParseFile(OpenFile.FileName);
            }
            else
            {
                select_file.Text = "请选择解析文件";
            }
        }

        private void ParseFile(string file)
        {
            if (File.Exists(file))
            {
                select_file.Text = file;
                bundles.Clear();

                try {
                    string name = null;
                    string size = null;
                    string unit = null;

                    string[] strs = null;

                    StreamReader sr = File.OpenText(file);
                    string line = null;
                    readState = ReadState.NONE;
                    char[] sep = { ' ', '\t' };

                    BundleInfo bundle = null;
                    while ((line = sr.ReadLine()) != null)
                    {

                        switch (readState)
                        {
                            case ReadState.NONE:
                                if (line.Contains("Bundle Name:"))
                                {
                                    readState = ReadState.BEGIN;
                                    name = line.Substring(line.IndexOf(':') + 2);
                                }
                                break;
                            case ReadState.BEGIN:
                                {
                                    int begin = line.IndexOf(':');
                                    int index = line.LastIndexOf(' ');
                                    size = line.Substring(begin + 1, index - begin - 1);
                                    unit = line.Substring(index + 1);
                                    bundle = new BundleInfo(name, size, unit);
                                    bundles.Add(bundle);

                                    line = sr.ReadLine();

                                    //Textures
                                    line = sr.ReadLine();
                                    strs = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                                    bundle.SetValue(ValueType.TEXTURES, strs[1], strs[2]);

                                    //Meshes
                                    line = sr.ReadLine();
                                    strs = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                                    bundle.SetValue(ValueType.MESHES, strs[1], strs[2]);

                                    //Animations
                                    line = sr.ReadLine();
                                    strs = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                                    bundle.SetValue(ValueType.ANIMATIONS, strs[1], strs[2]);

                                    //Sounds
                                    line = sr.ReadLine();
                                    strs = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                                    bundle.SetValue(ValueType.SOUNDS, strs[1], strs[2]);

                                    //Shaders
                                    line = sr.ReadLine();
                                    strs = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                                    bundle.SetValue(ValueType.SHADERS, strs[1], strs[2]);

                                    //Other Assets
                                    line = sr.ReadLine();
                                    strs = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                                    bundle.SetValue(ValueType.OTHER, strs[2], strs[3]);

                                    //Levels
                                    line = sr.ReadLine();
                                    strs = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                                    bundle.SetValue(ValueType.LEVELS, strs[1], strs[2]);

                                    //Scripts
                                    line = sr.ReadLine();

                                    //Included DLLs
                                    line = sr.ReadLine();

                                    //File headers
                                    line = sr.ReadLine();

                                    //Complete size
                                    line = sr.ReadLine();

                                    line = sr.ReadLine();
                                    line = sr.ReadLine();

                                    readState = ReadState.BUNDLE;
                                }
                                break;
                            case ReadState.BUNDLE:
                                if (line.Contains("------------"))
                                {
                                    readState = ReadState.END;
                                }
                                else
                                {
                                    if(!(line.EndsWith(".cs") || line.EndsWith(".dll")))
                                        bundle.AddAsset(line);
                                }
                                break;
                            case ReadState.END:
                                readState = ReadState.NONE;
                                break;
                        }
                    }

                    bundles.Sort(Sort);
                    count.Text = "Bundles : " + bundles.Count;
                    ShowList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace);
                    select_file.Text = "请选择解析文件";
                }
            }
        }

        private int Sort(BundleInfo b1, BundleInfo b2)
        {
            SingleInfo v1 = b1.GetValue(valueType);
            SingleInfo v2 = b2.GetValue(valueType);
            float vv1 = v1.Value * (v1.Unit == "kb" ? 1 : 1000);
            float vv2 = v2.Value * (v2.Unit == "kb" ? 1 : 1000);

            return vv2.CompareTo(vv1);
        }

        private void ShowList()
        {
            BundleList.Items.Clear();
            for (int i = 0; i < bundles.Count; ++i)
            {
                BundleInfo bundle = bundles[i];

                ListViewItem li = null;
                for (int j = 0; j < (int)ValueType.MAX_COUNT; ++j)
                {
                    if (j == 0)
                        li = new ListViewItem(bundle.Name);
                    else
                    {
                        SingleInfo info = bundle.GetInfoByType((ValueType)j);
                        li.SubItems.Add(string.Format("{0} {1}", info.Value, info.Unit));
                    }
                }
                BundleList.Items.Add(li);
            }
        }

        private void InitListView(ListView lv)
        {
            //添加列名
            ColumnHeader name = new ColumnHeader();
            name.Width = 190;
            name.Text = "名字";
            lv.Columns.Add(name);

            ColumnHeader size = new ColumnHeader();
            size.Width = 70;
            size.Text = "Size";
            lv.Columns.Add(size);

            ColumnHeader texture = new ColumnHeader();
            texture.Width = 70;
            texture.Text = "Texture";
            lv.Columns.Add(texture);

            ColumnHeader meshes = new ColumnHeader();
            meshes.Width = 70;
            meshes.Text = "mesh";
            lv.Columns.Add(meshes);

            ColumnHeader aniamtions = new ColumnHeader();
            aniamtions.Width = 70;
            aniamtions.Text = "Animation";
            lv.Columns.Add(aniamtions);

            ColumnHeader sounds = new ColumnHeader();
            sounds.Width = 70;
            sounds.Text = "Sound";
            lv.Columns.Add(sounds);

            ColumnHeader Shaders = new ColumnHeader();
            Shaders.Width = 70;
            Shaders.Text = "Shader";
            lv.Columns.Add(Shaders);

            ColumnHeader levels = new ColumnHeader();
            levels.Width = 70;
            levels.Text = "Level";
            lv.Columns.Add(levels);

            ColumnHeader other = new ColumnHeader();
            other.Width = 70;
            other.Text = "Other";
            lv.Columns.Add(other);

            //设置属性
            lv.GridLines = true;  //显示网格线
            lv.FullRowSelect = true;  //显示全行
            lv.MultiSelect = false;  //设置只能单选
            lv.View = View.Details;  //设置显示模式为详细
            lv.HoverSelection = false;  //当鼠标停留数秒后自动选择
        }

        private void InitAssetListView()
        {
            ColumnHeader name = new ColumnHeader();
            name.Width = 750;
            name.Text = "Asset";
            AssetsList.Columns.Add(name);

            //设置属性
            AssetsList.GridLines = true;  //显示网格线
            AssetsList.FullRowSelect = true;  //显示全行
            AssetsList.MultiSelect = false;  //设置只能单选
            AssetsList.View = View.Details;  //设置显示模式为详细
            AssetsList.HoverSelection = false;  //当鼠标停留数秒后自动选择
        }

        private void BundleList_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.C)
            {
                if (BundleList.SelectedItems.Count > 0)
                    Clipboard.SetDataObject(BundleList.SelectedItems[0].Text);
            }
        }

        private void BundleList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ValueType type = (ValueType)e.Column;
            if (type != valueType)
            {
                valueType = (ValueType)e.Column;
                bundles.Sort(Sort);
                ShowList();
            }
        }

        private void BundleList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = BundleList.Columns[e.ColumnIndex].Width;
        }

        private void BundleList_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && this.BundleList.SelectedItems.Count > 0)
            {
                this.menu.Show(this, e.Location);
            }
        }

        private void BundleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BundleList.SelectedItems.Count > 0)
            {
                BundleInfo bundle = bundles[BundleList.SelectedItems[0].Index];


                AssetsList.Items.Clear();
                for (int i = 0; i < bundle.Assets.Count; ++i)
                {
                    ListViewItem li = new ListViewItem(bundle.Assets[i]); ;
                    AssetsList.Items.Add(li);
                }
            }
        }
    }
}
