using System;

namespace BundleSizeAnalysis
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_select_file = new System.Windows.Forms.Button();
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.select_file = new System.Windows.Forms.Label();
            this.BundleList = new System.Windows.Forms.ListView();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.count = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AssetsList = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_select_file
            // 
            this.btn_select_file.Location = new System.Drawing.Point(114, 12);
            this.btn_select_file.Name = "btn_select_file";
            this.btn_select_file.Size = new System.Drawing.Size(75, 23);
            this.btn_select_file.TabIndex = 0;
            this.btn_select_file.Text = "选择文件";
            this.btn_select_file.UseVisualStyleBackColor = true;
            this.btn_select_file.Click += new System.EventHandler(this.btn_select_file_Click);
            // 
            // OpenFile
            // 
            this.OpenFile.FileName = "OpenFile";
            // 
            // select_file
            // 
            this.select_file.AutoSize = true;
            this.select_file.Location = new System.Drawing.Point(211, 17);
            this.select_file.Name = "select_file";
            this.select_file.Size = new System.Drawing.Size(89, 12);
            this.select_file.TabIndex = 1;
            this.select_file.Text = "请选择解析文件";
            // 
            // BundleList
            // 
            this.BundleList.ContextMenuStrip = this.menu;
            this.BundleList.HideSelection = false;
            this.BundleList.Location = new System.Drawing.Point(13, 41);
            this.BundleList.Name = "BundleList";
            this.BundleList.Size = new System.Drawing.Size(775, 397);
            this.BundleList.TabIndex = 4;
            this.BundleList.UseCompatibleStateImageBehavior = false;
            this.BundleList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.BundleList_ColumnClick);
            this.BundleList.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.BundleList_ColumnWidthChanging);
            this.BundleList.SelectedIndexChanged += new System.EventHandler(this.BundleList_SelectedIndexChanged);
            this.BundleList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BundleList_KeyUp);
            this.BundleList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BundleList_MouseClick);
            // 
            // menu
            // 
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(61, 4);
            // 
            // count
            // 
            this.count.AutoSize = true;
            this.count.Location = new System.Drawing.Point(700, 17);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(0, 12);
            this.count.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 445);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Assets";
            // 
            // AssetsList
            // 
            this.AssetsList.HideSelection = false;
            this.AssetsList.Location = new System.Drawing.Point(12, 461);
            this.AssetsList.Name = "AssetsList";
            this.AssetsList.Size = new System.Drawing.Size(776, 140);
            this.AssetsList.TabIndex = 7;
            this.AssetsList.UseCompatibleStateImageBehavior = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "重载文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 609);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AssetsList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.count);
            this.Controls.Add(this.BundleList);
            this.Controls.Add(this.select_file);
            this.Controls.Add(this.btn_select_file);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "BundleSizeAnalysis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_select_file;
        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.Label select_file;
        private System.Windows.Forms.ListView BundleList;
        private System.Windows.Forms.Label count;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView AssetsList;
        private System.Windows.Forms.Button button1;
    }
}

