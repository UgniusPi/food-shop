
namespace Food_Shop
{
    partial class OrderPreview
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblId = new System.Windows.Forms.Label();
            this.lblClientUsername = new System.Windows.Forms.Label();
            this.lblCourierUsername = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblDeliveryFee = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(14, 13);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(72, 18);
            this.lblId.TabIndex = 1;
            this.lblId.Text = "Order ID";
            // 
            // lblClientUsername
            // 
            this.lblClientUsername.AutoSize = true;
            this.lblClientUsername.Location = new System.Drawing.Point(14, 50);
            this.lblClientUsername.Name = "lblClientUsername";
            this.lblClientUsername.Size = new System.Drawing.Size(133, 18);
            this.lblClientUsername.TabIndex = 2;
            this.lblClientUsername.Text = "Client Username";
            // 
            // lblCourierUsername
            // 
            this.lblCourierUsername.AutoSize = true;
            this.lblCourierUsername.Location = new System.Drawing.Point(186, 13);
            this.lblCourierUsername.Name = "lblCourierUsername";
            this.lblCourierUsername.Size = new System.Drawing.Size(146, 18);
            this.lblCourierUsername.TabIndex = 3;
            this.lblCourierUsername.Text = "Courier Username";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(186, 50);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(116, 18);
            this.lblProductName.TabIndex = 4;
            this.lblProductName.Text = "Product Name";
            // 
            // lblDeliveryFee
            // 
            this.lblDeliveryFee.AutoSize = true;
            this.lblDeliveryFee.Location = new System.Drawing.Point(371, 13);
            this.lblDeliveryFee.Name = "lblDeliveryFee";
            this.lblDeliveryFee.Size = new System.Drawing.Size(101, 18);
            this.lblDeliveryFee.TabIndex = 5;
            this.lblDeliveryFee.Text = "Delivery Fee";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Red;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(553, 27);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(89, 31);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.AutoSize = true;
            this.lblTotalPrice.Location = new System.Drawing.Point(371, 50);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(90, 18);
            this.lblTotalPrice.TabIndex = 9;
            this.lblTotalPrice.Text = "Total Price";
            // 
            // OrderPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblTotalPrice);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblDeliveryFee);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.lblCourierUsername);
            this.Controls.Add(this.lblClientUsername);
            this.Controls.Add(this.lblId);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OrderPreview";
            this.Size = new System.Drawing.Size(658, 83);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblClientUsername;
        private System.Windows.Forms.Label lblCourierUsername;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblDeliveryFee;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblTotalPrice;
    }
}
