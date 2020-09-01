namespace VisionCS
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.PictureBoxMain = new System.Windows.Forms.PictureBox();
            this.label_message = new System.Windows.Forms.Label();
            this.ButtonYUVVision = new System.Windows.Forms.Button();
            this.ButtonRGBVision = new System.Windows.Forms.Button();
            this.ButtonVerMirroring = new System.Windows.Forms.Button();
            this.ButtonHorMirroring = new System.Windows.Forms.Button();
            this.ButtonEmbossFiltering = new System.Windows.Forms.Button();
            this.ButtonHistogramVision = new System.Windows.Forms.Button();
            this.ButtonLoadImage = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxMain
            // 
            this.PictureBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBoxMain.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxMain.Name = "PictureBoxMain";
            this.PictureBoxMain.Size = new System.Drawing.Size(563, 348);
            this.PictureBoxMain.TabIndex = 0;
            this.PictureBoxMain.TabStop = false;
            // 
            // label_message
            // 
            this.label_message.AutoSize = true;
            this.label_message.Location = new System.Drawing.Point(3, 354);
            this.label_message.Name = "label_message";
            this.label_message.Size = new System.Drawing.Size(106, 15);
            this.label_message.TabIndex = 1;
            this.label_message.Text = "load to Image!!";
            // 
            // ButtonYUVVision
            // 
            this.ButtonYUVVision.Location = new System.Drawing.Point(17, 422);
            this.ButtonYUVVision.Name = "ButtonYUVVision";
            this.ButtonYUVVision.Size = new System.Drawing.Size(255, 38);
            this.ButtonYUVVision.TabIndex = 2;
            this.ButtonYUVVision.Text = "YUV 평면 분할(3개파일 저장)";
            this.ButtonYUVVision.UseVisualStyleBackColor = true;
            this.ButtonYUVVision.Click += new System.EventHandler(this.ButtonYUVVision_Click);
            // 
            // ButtonRGBVision
            // 
            this.ButtonRGBVision.Location = new System.Drawing.Point(17, 466);
            this.ButtonRGBVision.Name = "ButtonRGBVision";
            this.ButtonRGBVision.Size = new System.Drawing.Size(255, 38);
            this.ButtonRGBVision.TabIndex = 3;
            this.ButtonRGBVision.Text = "RGB 평면 분할(3개파일 저장)";
            this.ButtonRGBVision.UseVisualStyleBackColor = true;
            this.ButtonRGBVision.Click += new System.EventHandler(this.ButtonRGBVision_Click);
            // 
            // ButtonVerMirroring
            // 
            this.ButtonVerMirroring.Location = new System.Drawing.Point(17, 510);
            this.ButtonVerMirroring.Name = "ButtonVerMirroring";
            this.ButtonVerMirroring.Size = new System.Drawing.Size(255, 38);
            this.ButtonVerMirroring.TabIndex = 4;
            this.ButtonVerMirroring.Text = "상하 대칭변환(1개파일 저장)";
            this.ButtonVerMirroring.UseVisualStyleBackColor = true;
            this.ButtonVerMirroring.Click += new System.EventHandler(this.ButtonVerMirroring_Click);
            // 
            // ButtonHorMirroring
            // 
            this.ButtonHorMirroring.Location = new System.Drawing.Point(17, 554);
            this.ButtonHorMirroring.Name = "ButtonHorMirroring";
            this.ButtonHorMirroring.Size = new System.Drawing.Size(255, 38);
            this.ButtonHorMirroring.TabIndex = 5;
            this.ButtonHorMirroring.Text = "좌우 대칭변환(1개파일 저장)";
            this.ButtonHorMirroring.UseVisualStyleBackColor = true;
            this.ButtonHorMirroring.Click += new System.EventHandler(this.ButtonHorMirroring_Click);
            // 
            // ButtonEmbossFiltering
            // 
            this.ButtonEmbossFiltering.Location = new System.Drawing.Point(17, 598);
            this.ButtonEmbossFiltering.Name = "ButtonEmbossFiltering";
            this.ButtonEmbossFiltering.Size = new System.Drawing.Size(255, 38);
            this.ButtonEmbossFiltering.TabIndex = 6;
            this.ButtonEmbossFiltering.Text = "엠보싱 필터링(1개파일 저장)";
            this.ButtonEmbossFiltering.UseVisualStyleBackColor = true;
            this.ButtonEmbossFiltering.Click += new System.EventHandler(this.ButtonEmbossFiltering_Click);
            // 
            // ButtonHistogramVision
            // 
            this.ButtonHistogramVision.Location = new System.Drawing.Point(17, 642);
            this.ButtonHistogramVision.Name = "ButtonHistogramVision";
            this.ButtonHistogramVision.Size = new System.Drawing.Size(255, 38);
            this.ButtonHistogramVision.TabIndex = 7;
            this.ButtonHistogramVision.Text = "Histogram 평활화(1개파일 저장)";
            this.ButtonHistogramVision.UseVisualStyleBackColor = true;
            this.ButtonHistogramVision.Click += new System.EventHandler(this.ButtonHistogramVision_Click);
            // 
            // ButtonLoadImage
            // 
            this.ButtonLoadImage.Location = new System.Drawing.Point(361, 598);
            this.ButtonLoadImage.Name = "ButtonLoadImage";
            this.ButtonLoadImage.Size = new System.Drawing.Size(190, 38);
            this.ButtonLoadImage.TabIndex = 8;
            this.ButtonLoadImage.Text = "이미지 읽어오기";
            this.ButtonLoadImage.UseVisualStyleBackColor = true;
            this.ButtonLoadImage.Click += new System.EventHandler(this.ButtonLoadImage_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(361, 642);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(190, 38);
            this.ButtonClose.TabIndex = 9;
            this.ButtonClose.Text = "종료";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(361, 354);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 25);
            this.dateTimePicker1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 735);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.ButtonLoadImage);
            this.Controls.Add(this.ButtonHistogramVision);
            this.Controls.Add(this.ButtonEmbossFiltering);
            this.Controls.Add(this.ButtonHorMirroring);
            this.Controls.Add(this.ButtonVerMirroring);
            this.Controls.Add(this.ButtonRGBVision);
            this.Controls.Add(this.ButtonYUVVision);
            this.Controls.Add(this.label_message);
            this.Controls.Add(this.PictureBoxMain);
            this.Name = "Form1";
            this.Text = "Image Processing";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxMain;
        private System.Windows.Forms.Label label_message;
        private System.Windows.Forms.Button ButtonYUVVision;
        private System.Windows.Forms.Button ButtonRGBVision;
        private System.Windows.Forms.Button ButtonVerMirroring;
        private System.Windows.Forms.Button ButtonHorMirroring;
        private System.Windows.Forms.Button ButtonEmbossFiltering;
        private System.Windows.Forms.Button ButtonHistogramVision;
        private System.Windows.Forms.Button ButtonLoadImage;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}

