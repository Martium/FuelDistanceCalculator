
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
            this.FuelPriceTextBox = new System.Windows.Forms.TextBox();
            this.FuelPriceLabel = new System.Windows.Forms.Label();
            this.ArrivalCityTextBox = new System.Windows.Forms.TextBox();
            this.ArrivalCityLabel = new System.Windows.Forms.Label();
            this.NationTextBox = new System.Windows.Forms.TextBox();
            this.NationLabel = new System.Windows.Forms.Label();
            this.Map = new GMap.NET.WindowsForms.GMapControl();
            this.MapContributorLinkLabel = new System.Windows.Forms.LinkLabel();
            this.InfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfoPanel
            // 
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
            this.InfoPanel.Controls.Add(this.FuelPriceTextBox);
            this.InfoPanel.Controls.Add(this.FuelPriceLabel);
            this.InfoPanel.Controls.Add(this.ArrivalCityTextBox);
            this.InfoPanel.Controls.Add(this.ArrivalCityLabel);
            this.InfoPanel.Controls.Add(this.NationTextBox);
            this.InfoPanel.Controls.Add(this.NationLabel);
            this.InfoPanel.Location = new System.Drawing.Point(-3, 0);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(347, 669);
            this.InfoPanel.TabIndex = 0;
            // 
            // ArrivalAddressTextBox
            // 
            this.ArrivalAddressTextBox.Location = new System.Drawing.Point(39, 152);
            this.ArrivalAddressTextBox.Name = "ArrivalAddressTextBox";
            this.ArrivalAddressTextBox.Size = new System.Drawing.Size(215, 20);
            this.ArrivalAddressTextBox.TabIndex = 19;
            // 
            // ArrivalAdressLabel
            // 
            this.ArrivalAdressLabel.AutoSize = true;
            this.ArrivalAdressLabel.Location = new System.Drawing.Point(40, 136);
            this.ArrivalAdressLabel.Name = "ArrivalAdressLabel";
            this.ArrivalAdressLabel.Size = new System.Drawing.Size(90, 13);
            this.ArrivalAdressLabel.TabIndex = 18;
            this.ArrivalAdressLabel.Text = "Atvykimo adresas";
            // 
            // DepartueAddressTextBox
            // 
            this.DepartueAddressTextBox.Location = new System.Drawing.Point(39, 74);
            this.DepartueAddressTextBox.Name = "DepartueAddressTextBox";
            this.DepartueAddressTextBox.Size = new System.Drawing.Size(215, 20);
            this.DepartueAddressTextBox.TabIndex = 17;
            // 
            // DepartureAddressLabel
            // 
            this.DepartureAddressLabel.AutoSize = true;
            this.DepartureAddressLabel.Location = new System.Drawing.Point(40, 58);
            this.DepartureAddressLabel.Name = "DepartureAddressLabel";
            this.DepartureAddressLabel.Size = new System.Drawing.Size(88, 13);
            this.DepartureAddressLabel.TabIndex = 16;
            this.DepartureAddressLabel.Text = "Išvykimo adresas";
            // 
            // AdditionalKmButton
            // 
            this.AdditionalKmButton.Location = new System.Drawing.Point(154, 566);
            this.AdditionalKmButton.Name = "AdditionalKmButton";
            this.AdditionalKmButton.Size = new System.Drawing.Size(100, 22);
            this.AdditionalKmButton.TabIndex = 15;
            this.AdditionalKmButton.Text = "Išsaugoti Km";
            this.AdditionalKmButton.UseVisualStyleBackColor = true;
            // 
            // SaveKmPriceButton
            // 
            this.SaveKmPriceButton.Location = new System.Drawing.Point(154, 527);
            this.SaveKmPriceButton.Name = "SaveKmPriceButton";
            this.SaveKmPriceButton.Size = new System.Drawing.Size(100, 22);
            this.SaveKmPriceButton.TabIndex = 14;
            this.SaveKmPriceButton.Text = "Išsaugoti kaina";
            this.SaveKmPriceButton.UseVisualStyleBackColor = true;
            // 
            // AdditionalKmLabel
            // 
            this.AdditionalKmLabel.AutoSize = true;
            this.AdditionalKmLabel.Location = new System.Drawing.Point(40, 552);
            this.AdditionalKmLabel.Name = "AdditionalKmLabel";
            this.AdditionalKmLabel.Size = new System.Drawing.Size(99, 13);
            this.AdditionalKmLabel.TabIndex = 13;
            this.AdditionalKmLabel.Text = "Papildomi kilometrai";
            // 
            // AdditionalKmTextBox
            // 
            this.AdditionalKmTextBox.Location = new System.Drawing.Point(39, 568);
            this.AdditionalKmTextBox.Name = "AdditionalKmTextBox";
            this.AdditionalKmTextBox.Size = new System.Drawing.Size(100, 20);
            this.AdditionalKmTextBox.TabIndex = 12;
            // 
            // TripPriceTextBox
            // 
            this.TripPriceTextBox.Location = new System.Drawing.Point(148, 328);
            this.TripPriceTextBox.Name = "TripPriceTextBox";
            this.TripPriceTextBox.Size = new System.Drawing.Size(106, 20);
            this.TripPriceTextBox.TabIndex = 11;
            // 
            // DistanceTextBox
            // 
            this.DistanceTextBox.Location = new System.Drawing.Point(148, 263);
            this.DistanceTextBox.Name = "DistanceTextBox";
            this.DistanceTextBox.Size = new System.Drawing.Size(106, 20);
            this.DistanceTextBox.TabIndex = 10;
            // 
            // TripPriceLabel
            // 
            this.TripPriceLabel.AutoSize = true;
            this.TripPriceLabel.Location = new System.Drawing.Point(40, 331);
            this.TripPriceLabel.Name = "TripPriceLabel";
            this.TripPriceLabel.Size = new System.Drawing.Size(76, 13);
            this.TripPriceLabel.TabIndex = 9;
            this.TripPriceLabel.Text = "Keliones kaina";
            // 
            // CalculateButton
            // 
            this.CalculateButton.Location = new System.Drawing.Point(148, 354);
            this.CalculateButton.Name = "CalculateButton";
            this.CalculateButton.Size = new System.Drawing.Size(70, 22);
            this.CalculateButton.TabIndex = 8;
            this.CalculateButton.Text = "skaičiuoti";
            this.CalculateButton.UseVisualStyleBackColor = true;
            // 
            // DistanceLabel
            // 
            this.DistanceLabel.AutoSize = true;
            this.DistanceLabel.Location = new System.Drawing.Point(40, 266);
            this.DistanceLabel.Name = "DistanceLabel";
            this.DistanceLabel.Size = new System.Drawing.Size(72, 13);
            this.DistanceLabel.TabIndex = 7;
            this.DistanceLabel.Text = "Atstumas \'Km\'";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(148, 178);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(70, 22);
            this.SearchButton.TabIndex = 6;
            this.SearchButton.Text = "Ieškoti";
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // FuelPriceTextBox
            // 
            this.FuelPriceTextBox.Location = new System.Drawing.Point(39, 529);
            this.FuelPriceTextBox.Name = "FuelPriceTextBox";
            this.FuelPriceTextBox.Size = new System.Drawing.Size(100, 20);
            this.FuelPriceTextBox.TabIndex = 5;
            // 
            // FuelPriceLabel
            // 
            this.FuelPriceLabel.AutoSize = true;
            this.FuelPriceLabel.Location = new System.Drawing.Point(40, 513);
            this.FuelPriceLabel.Name = "FuelPriceLabel";
            this.FuelPriceLabel.Size = new System.Drawing.Size(79, 13);
            this.FuelPriceLabel.TabIndex = 4;
            this.FuelPriceLabel.Text = "Kilometro kaina";
            // 
            // ArrivalCityTextBox
            // 
            this.ArrivalCityTextBox.Location = new System.Drawing.Point(39, 113);
            this.ArrivalCityTextBox.Name = "ArrivalCityTextBox";
            this.ArrivalCityTextBox.Size = new System.Drawing.Size(215, 20);
            this.ArrivalCityTextBox.TabIndex = 3;
            // 
            // ArrivalCityLabel
            // 
            this.ArrivalCityLabel.AutoSize = true;
            this.ArrivalCityLabel.Location = new System.Drawing.Point(40, 97);
            this.ArrivalCityLabel.Name = "ArrivalCityLabel";
            this.ArrivalCityLabel.Size = new System.Drawing.Size(88, 13);
            this.ArrivalCityLabel.TabIndex = 2;
            this.ArrivalCityLabel.Text = "Atvykimo miestas";
            // 
            // NationTextBox
            // 
            this.NationTextBox.Location = new System.Drawing.Point(39, 490);
            this.NationTextBox.Name = "NationTextBox";
            this.NationTextBox.Size = new System.Drawing.Size(100, 20);
            this.NationTextBox.TabIndex = 1;
            // 
            // NationLabel
            // 
            this.NationLabel.AutoSize = true;
            this.NationLabel.Location = new System.Drawing.Point(40, 474);
            this.NationLabel.Name = "NationLabel";
            this.NationLabel.Size = new System.Drawing.Size(29, 13);
            this.NationLabel.TabIndex = 0;
            this.NationLabel.Text = "Šalis";
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
            this.MapContributorLinkLabel.Location = new System.Drawing.Point(661, 644);
            this.MapContributorLinkLabel.Name = "MapContributorLinkLabel";
            this.MapContributorLinkLabel.Size = new System.Drawing.Size(122, 13);
            this.MapContributorLinkLabel.TabIndex = 2;
            this.MapContributorLinkLabel.TabStop = true;
            this.MapContributorLinkLabel.Text = "www.openstreetmap.org";
            this.MapContributorLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MapContributorLinkLabel_LinkClicked);
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
        private System.Windows.Forms.TextBox FuelPriceTextBox;
        private System.Windows.Forms.Label FuelPriceLabel;
        private System.Windows.Forms.TextBox ArrivalCityTextBox;
        private System.Windows.Forms.Label ArrivalCityLabel;
        private System.Windows.Forms.TextBox NationTextBox;
        private System.Windows.Forms.Label NationLabel;
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
    }
}

