namespace WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void SumButton_ClickAsync(object sender, EventArgs e)
        {
            using HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync($"http://localhost:5277/BDA/sum?X={textBox1.Text}&Y={textBox2.Text}", new StringContent(string.Empty));
            var responseText = await response.Content.ReadAsStringAsync();
            textBox3.Text = responseText;
        }
    }
}
