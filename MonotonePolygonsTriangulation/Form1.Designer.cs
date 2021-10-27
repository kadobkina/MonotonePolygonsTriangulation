
namespace MonotonePolygonsTriangulation
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonPolygon = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.buttonTriangulation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPolygon
            // 
            this.buttonPolygon.Location = new System.Drawing.Point(12, 29);
            this.buttonPolygon.Name = "buttonPolygon";
            this.buttonPolygon.Size = new System.Drawing.Size(138, 23);
            this.buttonPolygon.TabIndex = 0;
            this.buttonPolygon.Text = "Нарисовать полигон";
            this.buttonPolygon.UseVisualStyleBackColor = true;
            this.buttonPolygon.Click += new System.EventHandler(this.buttonPolygon_Click);
            // 
            // canvas
            // 
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(168, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(620, 426);
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            this.canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseClick);
            // 
            // buttonTriangulation
            // 
            this.buttonTriangulation.Enabled = false;
            this.buttonTriangulation.Location = new System.Drawing.Point(12, 58);
            this.buttonTriangulation.Name = "buttonTriangulation";
            this.buttonTriangulation.Size = new System.Drawing.Size(138, 23);
            this.buttonTriangulation.TabIndex = 2;
            this.buttonTriangulation.Text = "Триангуляция";
            this.buttonTriangulation.UseVisualStyleBackColor = true;
            this.buttonTriangulation.Click += new System.EventHandler(this.buttonTriangulation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonTriangulation);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.buttonPolygon);
            this.Name = "Form1";
            this.Text = "Триангуляция монотонных полигонов";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPolygon;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button buttonTriangulation;
        private System.Windows.Forms.Label label1;
    }
}

