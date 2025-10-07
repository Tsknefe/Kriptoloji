namespace Kriptoloji
{
    public partial class Form1 : Form
    {
        private ComboBox cmbAlgoritma;
        private TextBox txtMetin;
        private TextBox txtAnahtar;
        private Button btnSifrele;
        private Button btnCoz;
        private TextBox txtSonuc;
        private Label lblSonuc;

        private Dictionary<string, Ibase> algoritmalar = new Dictionary<string, Ibase>()
        {
            { "Kaydýrmalý (Caesar)", new Kaydýrmalý() },
            { "Doðrusal (Affine)", new Doðrusal() },
            { "Yer Deðiþtirme", new Yerdegistirme() },
            { "Permütasyon", new Permutasyon() },
            { "Vigenère", new Vigenere() },
            { "4 Kare", new _4Kare() },
            { "Anahtarlý Yer Deðiþtirme", new AnahtarlýKaydýrma() },
            { "Rota (Spiral)", new Rota() },
            { "Zigzag", new Zigzag() },
            { "Hill", new Hill() }
        };

        public Form1()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            this.Text = "Kriptoloji Arayüzü";
            this.Size = new Size(700, 400);

            cmbAlgoritma = new ComboBox()
            {
                Location = new Point(20, 20),
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbAlgoritma.Items.AddRange(algoritmalar.Keys.ToArray());
            cmbAlgoritma.SelectedIndex = 0;

            txtMetin = new TextBox()
            {
                Location = new Point(20, 60),
                Width = 650,
                Height = 60,
                Multiline = true
            };

            txtAnahtar = new TextBox()
            {
                Location = new Point(20, 130),
                Width = 300,
                PlaceholderText = "Anahtar (Sayý girilmesi gerekenlerde sayý girin)"
            };

            btnSifrele = new Button()
            {
                Text = "Þifrele",
                Location = new Point(350, 130),
                Width = 100
            };
            btnSifrele.Click += BtnSifrele_Click;

            btnCoz = new Button()
            {
                Text = "Çöz",
                Location = new Point(470, 130),
                Width = 100
            };
            btnCoz.Click += BtnCoz_Click;

            lblSonuc = new Label()
            {
                Text = "Sonuç:",
                Location = new Point(20, 180),
                AutoSize = true
            };

            txtSonuc = new TextBox()
            {
                Location = new Point(20, 210),
                Width = 650,
                Height = 100,
                Multiline = true,
                ReadOnly = true
            };

            this.Controls.Add(cmbAlgoritma);
            this.Controls.Add(txtMetin);
            this.Controls.Add(txtAnahtar);
            this.Controls.Add(btnSifrele);
            this.Controls.Add(btnCoz);
            this.Controls.Add(lblSonuc);
            this.Controls.Add(txtSonuc);
        }

        private void BtnSifrele_Click(object sender, EventArgs e)
        {
            try
            {
                string metin = txtMetin.Text;
                string anahtar = txtAnahtar.Text;
                string secim = cmbAlgoritma.SelectedItem.ToString();

                var algoritma = algoritmalar[secim];
                string sonuc = algoritma.Encrypt(metin, anahtar);
                txtSonuc.Text = sonuc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void BtnCoz_Click(object sender, EventArgs e)
        {
            try
            {
                string metin = txtMetin.Text;
                string anahtar = txtAnahtar.Text;
                string secim = cmbAlgoritma.SelectedItem.ToString();

                var algoritma = algoritmalar[secim];
                string sonuc = algoritma.Decrypt(metin, anahtar);
                txtSonuc.Text = sonuc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}