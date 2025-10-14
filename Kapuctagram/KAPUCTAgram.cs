using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KAPUCTAgram
{
    public partial class KAPUCTAgram : Form
    {
        private string username;
        private TcpClient client;      // без ? — для C# 7.3
        private NetworkStream stream;  // без ? — для C# 7.3

        public KAPUCTAgram(string username)
        {
            InitializeComponent();
            this.username = username;

            // Настройка ChatBox
            ChatBox.Multiline = true;
            ChatBox.ReadOnly = true;
            ChatBox.ScrollBars = ScrollBars.Vertical;

            // Подключение обработчиков
            MessageTB.KeyDown += MessageTB_KeyDown;
            MessageTB.TextChanged += MessageTB_TextChanged;

            // Запуск подключения к серверу
            _ = ConnectToServerAsync();
        }

        // ===========================
        // Сетевая логика
        // ===========================

        private async Task ConnectToServerAsync()
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync("localhost", 8888); // ← замени на IP сервера в сети, если нужно
                stream = client.GetStream();

                AppendMessage("✅ Подключено к чату.");
                _ = Task.Run(ReceiveMessagesLoop);
            }
            catch (Exception ex)
            {
                AppendMessage($"❌ Не удалось подключиться: {ex.Message}");
            }
        }

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
                    // Обновляем UI в основном потоке
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

        private void SendMessage()
        {
            if (client == null || !client.Connected || string.IsNullOrWhiteSpace(MessageTB.Text))
                return;

            string text = MessageTB.Text.Trim();
            string fullMessage = $"[{username}]: {text}";
            byte[] data = Encoding.UTF8.GetBytes(fullMessage);

            try
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
                AppendMessage($"[Вы]: {text}");
                MessageTB.Text = "";
            }
            catch (Exception ex)
            {
                AppendMessage($"❌ Ошибка отправки: {ex.Message}");
            }
        }

        private void AppendMessage(string message)
        {
            ChatBox.AppendText($"{message}{Environment.NewLine}");
            ChatBox.ScrollToCaret();
        }

        // ===========================
        // Обработка ввода
        // ===========================

        private void MessageTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Shift)
                {
                    // Разрешаем новую строку
                    e.SuppressKeyPress = false;
                }
                else
                {
                    // Отправляем сообщение
                    e.SuppressKeyPress = true;
                    SendMessage();
                }
            }
        }

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

        // ===========================
        // Закрытие формы
        // ===========================

        private void KAPUCTAgram_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                stream?.Close();
                client?.Close();
            }
            catch { /* игнор */ }
        }
    }
}