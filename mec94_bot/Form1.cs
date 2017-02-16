using NetTelegramBotApi;
using NetTelegramBotApi.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mec94_bot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.InitializeLifetimeService();
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Task.Run(() => Runbot());
        }
        public static async Task Runbot()
        {
            var bot = new TelegramBot("339493278:AAH-4bouLgr2RGNhiqBujxSdyYUw2M5wPGU");
            long offset = 0;
            while (true)
            {
                var updates = await bot.MakeRequestAsync(new GetUpdates() { Offset = offset });
                foreach (var update in updates)
                {
                    offset = update.UpdateId + 1;
                    var text = update.Message.Text;
                    var file = update.Message.Document;
                    if (file != null)
                    {
                        var req = new SendMessage(update.Message.Chat.Id, "File ID: " + file.FileId.ToString());
                        await bot.MakeRequestAsync(req);
                        continue;
                    }
                    else if (text != null)
                    {
                        var req = new SendMessage(update.Message.Chat.Id, "ID: " + update.Message.ForwardFrom.Id);
                        await bot.MakeRequestAsync(req);
                        continue;
                    }

                }
            }
        }
    }
}