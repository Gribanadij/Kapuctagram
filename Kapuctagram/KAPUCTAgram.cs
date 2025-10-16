using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KAPUCTAgram
{
    public partial class KAPUCTAgram : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        // === ДОБАВЛЕНО: поля для хранения данных пользователя ===
        private readonly string _userPassword;
        private readonly string _userName;
        private readonly string _serverIp;
        // ======================================================

        // === ИЗМЕНЁН: конструктор сохраняет данные ===
        public KAPUCTAgram(string password, string name, string serverIp)
        {
            InitializeComponent();
            _userPassword = password;
            _userName = name;
            _serverIp = serverIp;
            this.Text = $"KAPUCTAgram — {name}";
        }

        // === ДОБАВЛЕНО: подключение при загрузке формы ===
        private async void KAPUCTAgram_Load(object sender, EventArgs e)
        {
            await ConnectAndAuthenticate();
        }

        // === ДОБАВЛЕНО: метод подключения с обработкой ошибок ===
        private async Task ConnectAndAuthenticate()
        {
            try
            {
                AppendMessage("🔌 Подключение к серверу...");
                client = new TcpClient(_serverIp, 8888);
                stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream) { AutoFlush = true };

                // Отправляем данные для входа
                await writer.WriteLineAsync($"AUTH:{_userPassword}|{_userName}");

                // Ждём ответ от сервера
                string response = await reader.ReadLineAsync();
                if (response == "OK")
                {
                    AppendMessage("✅ Вход выполнен успешно!");
                    // Запускаем приём сообщений (включая историю)
                    _ = ReceiveMessagesLoop();
                }
                else
                {
                    AppendMessage($"❌ Ошибка сервера: {response}");
                    MessageBox.Show("Не удалось войти на сервер.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
            catch (Exception ex)
            {
                AppendMessage($"❌ Ошибка подключения: {ex.Message}");
                MessageBox.Show($"Не удалось подключиться к серверу:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        // === ДОБАВЛЕНО: обработка нажатия Enter в поле ввода ===
        private void MessageTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true; // отменяет добавление новой строки
                SendMessageButton_Click(sender, e); // вызываем отправку
            }
        }

        // === СОХРАНЕНА ТВОЯ ЛОГИКА ОТПРАВКИ ===
        private async void SendMessageButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MessageTB.Text)) return;

            try
            {
                if (writer != null)
                {
                    // Формат сообщения: [Имя]: Текст
                    string message = $"{_userName}: {MessageTB.Text}";
                    await writer.WriteLineAsync(message);
                    MessageTB.Clear();
                }
            }
            catch (Exception ex)
            {
                AppendMessage($"❌ Ошибка отправки: {ex.Message}");
            }
        }

        // === СОХРАНЕНА ТВОЯ ЛОГИКА ПРИЁМА ===
        private async Task ReceiveMessagesLoop()
        {
            byte[] buffer = new byte[1024];
            try
            {
                while (client != null && client.Connected && stream != null)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    if (InvokeRequired)
                    {
                        Invoke(new Action<string>(AppendMessage), message);
                    }
                    else
                    {
                        AppendMessage(message);
                    }
                }
            }
            catch (Exception ex)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(AppendMessage), $"⚠️ Отключено: {ex.Message}");
                }
                else
                {
                    AppendMessage($"⚠️ Отключено: {ex.Message}");
                }
            }
        }

        // === СОХРАНЕНА ТВОЯ ЛОГИКА ДОБАВЛЕНИЯ СООБЩЕНИЙ ===
        private void AppendMessage(string message)
        {
            ChatBox.AppendText($"{message}{Environment.NewLine}");
            ChatBox.ScrollToCaret();
        }

        // === СОХРАНЕНА ТВОЯ ЛОГИКА ИЗМЕНЕНИЯ ВЫСОТЫ ===
        private void MessageTB_TextChanged(object sender, EventArgs e)
        {
            BeginInvoke(new Action(AdjustInputBoxHeight));
        }

        private void AdjustInputBoxHeight()
        {
            const int maxHeight = 120;
            const int padding = 8;

            string text = MessageTB.Text;
            int lineCount = 1;

            if (!string.IsNullOrEmpty(text))
            {
                lineCount = text.Split(new string[] { "\r\n" }, StringSplitOptions.None).Length;
                if (text.EndsWith("\r\n"))
                    lineCount++;
            }

            int newHeight = (MessageTB.Font.Height * lineCount) + padding;
            newHeight = Math.Max(newHeight, MessageTB.Font.Height + padding);
            newHeight = Math.Min(newHeight, maxHeight);

            int bottom = MessageTB.Top + MessageTB.Height;
            MessageTB.Height = newHeight;
            MessageTB.Top = bottom - MessageTB.Height;
        }

        // === ДОБАВЛЕНО: корректное закрытие соединения ===
        private void KAPUCTAgram_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                writer?.Close();
                reader?.Close();
                client?.Close();
            }
            catch { }
        }
    }
}