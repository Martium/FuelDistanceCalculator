
namespace Martium.TravelInfo.Forms
{
    partial class TravelInfoForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.ArrivalAddressTextBox = new System.Windows.Forms.TextBox();
            this.ArrivalAdressLabel = new System.Windows.Forms.Label();
            this.DepartueAddressTextBox = new System.Windows.Forms.TextBox();
            this.DepartureAddressLabel = new System.Windows.Forms.Label();
            this.AdditionalKmButton = new System.Windows.Forms.Button();
            this.SaveKmPriceButton = new System.Windows.Forms.Button();
            this.AdditionalKmLabel = new System.Windows.Forms.Label();
            this.AdditionalKmTextBox = new System.Windows.Forms.TextBox();
            this.TripPriceTextBox = new System.Windows.Forms.TextBox();
            this.DistanceTextBox = new System.Windows.Forms.TextBox();
            this.TripPriceLabel = new System.Windows.Forms.Label();
            this.CalculateButton = new System.Windows.Forms.Button();
            this.DistanceLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.KmPriceTextBox = new System.Windows.Forms.TextBox();
            this.FuelPriceLabel = new System.Windows.Forms.Label();
            this.DepartureCountryTextBox = new System.Windows.Forms.TextBox();
            this.DepartureCountryLabel = new System.Windows.Forms.Label();
            this.Map = new GMap.NET.WindowsForms.GMapControl();
            this.MapContributorLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SaveDepartureAddressButton = new System.Windows.Forms.Button();
            this.InfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfoPanel
            // 
            this.InfoPanel.Controls.Add(this.SaveDepartureAddressButton);
            this.InfoPanel.Controls.Add(this.ArrivalAddressTextBox);
            this.InfoPanel.Controls.Add(this.ArrivalAdressLabel);
            this.InfoPanel.Controls.Add(this.DepartueAddressTextBox);
            this.InfoPanel.Controls.Add(this.DepartureAddressLabel);
            this.InfoPanel.Controls.Add(this.AdditionalKmButton);
            this.InfoPanel.Controls.Add(this.SaveKmPriceButton);
            this.InfoPanel.Controls.Add(this.AdditionalKmLabel);
            this.InfoPanel.Controls.Add(this.AdditionalKmTextBox);
            this.InfoPanel.Controls.Add(this.TripPriceTextBox);
            this.InfoPanel.Controls.Add(this.DistanceTextBox);
            this.InfoPanel.Controls.Add(this.TripPriceLabel);
            this.InfoPanel.Controls.Add(this.CalculateButton);
            this.InfoPanel.Controls.Add(this.DistanceLabel);
            this.InfoPanel.Controls.Add(this.SearchButton);
            this.InfoPanel.Controls.Add(this.KmPriceTextBox);
            this.InfoPanel.Controls.Add(this.FuelPriceLabel);
            this.InfoPanel.Controls.Add(this.DepartureCountryTextBox);
            this.InfoPanel.Controls.Add(this.DepartureCountryLabel);
            this.InfoPanel.Location = new System.Drawing.Point(-3, 0);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(347, 669);
            this.InfoPanel.TabIndex = 0;
            // 
            // ArrivalAddressTextBox
            // 
            this.ArrivalAddressTextBox.Location = new System.Drawing.Point(39, 150);
            this.ArrivalAddressTextBox.Name = "ArrivalAddressTextBox";
            this.ArrivalAddressTextBox.Size = new System.Drawing.Size(215, 20);
            this.ArrivalAddressTextBox.TabIndex = 19;
            // 
            // ArrivalAdressLabel
            // 
            this.ArrivalAdressLabel.AutoSize = true;
            this.ArrivalAdressLabel.Location = new System.Drawing.Point(40, 134);
            this.ArrivalAdressLabel.Name = "ArrivalAdressLabel";
            this.ArrivalAdressLabel.Size = new System.Drawing.Size(90, 13);
            this.ArrivalAdressLabel.TabIndex = 18;
            this.ArrivalAdressLabel.Text = "Atvykimo adresas";
            // 
            // DepartueAddressTextBox
            // 
            this.DepartueAddressTextBox.Location = new System.Drawing.Point(39, 111);
            this.DepartueAddressTextBox.Name = "DepartueAddressTextBox";
            this.DepartueAddressTextBox.Size = new System.Drawing.Size(215, 20);
            this.DepartueAddressTextBox.TabIndex = 17;
            // 
            // DepartureAddressLabel
            // 
            this.DepartureAddressLabel.AutoSize = true;
            this.DepartureAddressLabel.Location = new System.Drawing.Point(40, 95);
            this.DepartureAddressLabel.Name = "DepartureAddressLabel";
            this.DepartureAddressLabel.Size = new System.Drawing.Size(88, 13);
            this.DepartureAddressLabel.TabIndex = 16;
            this.DepartureAddressLabel.Text = "Išvykimo adresas";
            // 
            // AdditionalKmButton
            // 
            this.AdditionalKmButton.Location = new System.Drawing.Point(145, 322);
            this.AdditionalKmButton.Name = "AdditionalKmButton";
            this.AdditionalKmButton.Size = new System.Drawing.Size(71, 22);
            this.AdditionalKmButton.TabIndex = 15;
            this.AdditionalKmButton.Text = "Išsaugoti";
            this.AdditionalKmButton.UseVisualStyleBackColor = true;
            // 
            // SaveKmPriceButton
            // 
            this.SaveKmPriceButton.Location = new System.Drawing.Point(145, 283);
            this.SaveKmPriceButton.Name = "SaveKmPriceButton";
            this.SaveKmPriceButton.Size = new System.Drawing.Size(71, 22);
            this.SaveKmPriceButton.TabIndex = 14;
            this.SaveKmPriceButton.Text = "Išsaugoti";
            this.SaveKmPriceButton.UseVisualStyleBackColor = true;
            // 
            // AdditionalKmLabel
            // 
            this.AdditionalKmLabel.AutoSize = true;
            this.AdditionalKmLabel.Location = new System.Drawing.Point(40, 308);
            this.AdditionalKmLabel.Name = "AdditionalKmLabel";
            this.AdditionalKmLabel.Size = new System.Drawing.Size(99, 13);
            this.AdditionalKmLabel.TabIndex = 13;
            this.AdditionalKmLabel.Text = "Papildomi kilometrai";
            // 
            // AdditionalKmTextBox
            // 
            this.AdditionalKmTextBox.Location = new System.Drawing.Point(39, 324);
            this.AdditionalKmTextBox.Name = "AdditionalKmTextBox";
            this.AdditionalKmTextBox.Size = new System.Drawing.Size(100, 20);
            this.AdditionalKmTextBox.TabIndex = 12;
            // 
            // TripPriceTextBox
            // 
            this.TripPriceTextBox.Location = new System.Drawing.Point(119, 507);
            this.TripPriceTextBox.Name = "TripPriceTextBox";
            this.TripPriceTextBox.Size = new System.Drawing.Size(106, 20);
            this.TripPriceTextBox.TabIndex = 11;
            // 
            // DistanceTextBox
            // 
            this.DistanceTextBox.Location = new System.Drawing.Point(119, 474);
            this.DistanceTextBox.Name = "DistanceTextBox";
            this.DistanceTextBox.Size = new System.Drawing.Size(106, 20);
            this.DistanceTextBox.TabIndex = 10;
            // 
            // TripPriceLabel
            // 
            this.TripPriceLabel.AutoSize = true;
            this.TripPriceLabel.Location = new System.Drawing.Point(40, 510);
            this.TripPriceLabel.Name = "TripPriceLabel";
            this.TripPriceLabel.Size = new System.Drawing.Size(37, 13);
            this.TripPriceLabel.TabIndex = 9;
            this.TripPriceLabel.Text = "Kaina:";
            // 
            // CalculateButton
            // 
            this.CalculateButton.Location = new System.Drawing.Point(39, 424);
            this.CalculateButton.Name = "CalculateButton";
            this.CalculateButton.Size = new System.Drawing.Size(94, 40);
            this.CalculateButton.TabIndex = 8;
            this.CalculateButton.Text = "Skaičiuoti kelionės kainą";
            this.CalculateButton.UseVisualStyleBackColor = true;
            // 
            // DistanceLabel
            // 
            this.DistanceLabel.AutoSize = true;
            this.DistanceLabel.Location = new System.Drawing.Point(40, 477);
            this.DistanceLabel.Name = "DistanceLabel";
            this.DistanceLabel.Size = new System.Drawing.Size(73, 13);
            this.DistanceLabel.TabIndex = 7;
            this.DistanceLabel.Text = "Atstumas, km:";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(39, 176);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(70, 22);
            this.SearchButton.TabIndex = 6;
            this.SearchButton.Text = "Ieškoti";
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // KmPriceTextBox
            // 
            this.KmPriceTextBox.Location = new System.Drawing.Point(39, 285);
            this.KmPriceTextBox.Name = "KmPriceTextBox";
            this.KmPriceTextBox.Size = new System.Drawing.Size(100, 20);
            this.KmPriceTextBox.TabIndex = 5;
            // 
            // FuelPriceLabel
            // 
            this.FuelPriceLabel.AutoSize = true;
            this.FuelPriceLabel.Location = new System.Drawing.Point(40, 269);
            this.FuelPriceLabel.Name = "FuelPriceLabel";
            this.FuelPriceLabel.Size = new System.Drawing.Size(79, 13);
            this.FuelPriceLabel.TabIndex = 4;
            this.FuelPriceLabel.Text = "Kilometro kaina";
            // 
            // DepartureCountryTextBox
            // 
            this.DepartureCountryTextBox.Location = new System.Drawing.Point(39, 37);
            this.DepartureCountryTextBox.Name = "DepartureCountryTextBox";
            this.DepartureCountryTextBox.Size = new System.Drawing.Size(100, 20);
            this.DepartureCountryTextBox.TabIndex = 1;
            // 
            // DepartureCountryLabel
            // 
            this.DepartureCountryLabel.AutoSize = true;
            this.DepartureCountryLabel.Location = new System.Drawing.Point(40, 21);
            this.DepartureCountryLabel.Name = "DepartureCountryLabel";
            this.DepartureCountryLabel.Size = new System.Drawing.Size(71, 13);
            this.DepartureCountryLabel.TabIndex = 0;
            this.DepartureCountryLabel.Text = "Išvykimo šalis";
            // 
            // Map
            // 
            this.Map.Bearing = 0F;
            this.Map.CanDragMap = true;
            this.Map.EmptyTileColor = System.Drawing.Color.Navy;
            this.Map.GrayScaleMode = false;
            this.Map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.Map.LevelsKeepInMemory = 5;
            this.Map.Location = new System.Drawing.Point(344, 0);
            this.Map.MarkersEnabled = true;
            this.Map.MaxZoom = 18;
            this.Map.MinZoom = 2;
            this.Map.MouseWheelZoomEnabled = true;
            this.Map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.Map.Name = "Map";
            this.Map.NegativeMode = false;
            this.Map.PolygonsEnabled = true;
            this.Map.RetryLoadTile = 0;
            this.Map.RoutesEnabled = true;
            this.Map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.Map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.Map.ShowTileGridLines = false;
            this.Map.Size = new System.Drawing.Size(724, 641);
            this.Map.TabIndex = 1;
            this.Map.Zoom = 13D;
            // 
            // MapContributorLinkLabel
            // 
            this.MapContributorLinkLabel.AutoSize = true;
            this.MapContributorLinkLabel.Location = new System.Drawing.Point(641, 644);
            this.MapContributorLinkLabel.Name = "MapContributorLinkLabel";
            this.MapContributorLinkLabel.Size = new System.Drawing.Size(152, 13);
            this.MapContributorLinkLabel.TabIndex = 2;
            this.MapContributorLinkLabel.TabStop = true;
            this.MapContributorLinkLabel.Text = "© OpenStreetMap contributors";
            this.MapContributorLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MapContributorLinkLabel_LinkClicked);
            // 
            // SaveDepartureAddressButton
            // 
            this.SaveDepartureAddressButton.Location = new System.Drawing.Point(260, 111);
            this.SaveDepartureAddressButton.Name = "SaveDepartureAddressButton";
            this.SaveDepartureAddressButton.Size = new System.Drawing.Size(71, 22);
            this.SaveDepartureAddressButton.TabIndex = 20;
            this.SaveDepartureAddressButton.Text = "Išsaugoti";
            this.SaveDepartureAddressButton.UseVisualStyleBackColor = true;
            // 
            // TravelInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1070, 666);
            this.Controls.Add(this.MapContributorLinkLabel);
            this.Controls.Add(this.Map);
            this.Controls.Add(this.InfoPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TravelInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kelionės skaičiuoklė";
            this.InfoPanel.ResumeLayout(false);
            this.InfoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.TextBox KmPriceTextBox;
        private System.Windows.Forms.Label FuelPriceLabel;
        private System.Windows.Forms.TextBox DepartureCountryTextBox;
        private System.Windows.Forms.Label DepartureCountryLabel;
        private System.Windows.Forms.Label DistanceLabel;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox DistanceTextBox;
        private System.Windows.Forms.Label TripPriceLabel;
        private System.Windows.Forms.Button CalculateButton;
        private System.Windows.Forms.TextBox TripPriceTextBox;
        private System.Windows.Forms.Button AdditionalKmButton;
        private System.Windows.Forms.Button SaveKmPriceButton;
        private System.Windows.Forms.Label AdditionalKmLabel;
        private System.Windows.Forms.TextBox AdditionalKmTextBox;
        private System.Windows.Forms.Label ArrivalAdressLabel;
        private System.Windows.Forms.TextBox DepartueAddressTextBox;
        private System.Windows.Forms.Label DepartureAddressLabel;
        private System.Windows.Forms.TextBox ArrivalAddressTextBox;
        private GMap.NET.WindowsForms.GMapControl Map;
        private System.Windows.Forms.LinkLabel MapContributorLinkLabel;
        private System.Windows.Forms.Button SaveDepartureAddressButton;
    }
}

