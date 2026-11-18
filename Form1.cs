using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeyScope;

public class Form1 : Form
{
    private readonly EventLogger logger = new EventLogger();
    private readonly Label statusLabel = new Label();
    private readonly Label countLabel = new Label();
    private readonly Label lastKeyLabel = new Label();
    private readonly ListBox recordList = new ListBox();
    private readonly Button startButton = new Button();
    private readonly Button stopButton = new Button();

    public Form1()
    {
        Text = "KeyScope — Klavye Olayları";
        ClientSize = new Size(880, 600);
        MinimumSize = new Size(760, 540);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(15, 23, 42);
        ForeColor = Color.FromArgb(226, 232, 240);
        Font = new Font("Segoe UI", 10);
        AutoScaleMode = AutoScaleMode.Dpi;

        // Form, odak alt kontroldeyken de KeyDown olayını önce alır.
        KeyPreview = true;
        KeyDown += Form1_KeyDown;

        Label titleLabel = new Label();
        titleLabel.Text = "KeyScope";
        titleLabel.Font = new Font("Segoe UI", 26, FontStyle.Bold);
        titleLabel.SetBounds(28, 20, 350, 54);
        Controls.Add(titleLabel);

        Label subtitleLabel = new Label();
        subtitleLabel.Text = "KLAVYE OLAYLARI  /  YMH219";
        subtitleLabel.ForeColor = Color.FromArgb(148, 163, 184);
        subtitleLabel.SetBounds(31, 80, 500, 28);
        Controls.Add(subtitleLabel);

        Panel summaryPanel = new Panel();
        summaryPanel.BackColor = Color.FromArgb(30, 41, 59);
        summaryPanel.SetBounds(28, 124, 824, 92);
        summaryPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        Controls.Add(summaryPanel);

        statusLabel.SetBounds(18, 16, 310, 28);
        statusLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        summaryPanel.Controls.Add(statusLabel);

        countLabel.SetBounds(18, 52, 310, 26);
        summaryPanel.Controls.Add(countLabel);
        lastKeyLabel.SetBounds(350, 52, 370, 26);
        summaryPanel.Controls.Add(lastKeyLabel);

        Label listTitle = new Label();
        listTitle.Text = "OLAY KAYITLARI";
        listTitle.ForeColor = Color.FromArgb(148, 163, 184);
        listTitle.SetBounds(28, 234, 300, 24);
        Controls.Add(listTitle);

        recordList.SetBounds(28, 266, 824, 208);
        recordList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        recordList.BackColor = Color.FromArgb(20, 30, 48);
        recordList.ForeColor = Color.FromArgb(226, 232, 240);
        recordList.Font = new Font("Consolas", 11);
        recordList.BorderStyle = BorderStyle.FixedSingle;
        recordList.IntegralHeight = false;
        recordList.HorizontalScrollbar = true;
        Controls.Add(recordList);

        ConfigureButton(startButton, "Başlat", 28, Color.FromArgb(21, 128, 61));
        startButton.Click += StartButton_Click;
        ConfigureButton(stopButton, "Durdur", 174, Color.FromArgb(185, 28, 28));
        stopButton.Click += StopButton_Click;

        Button clearButton = new Button();
        ConfigureButton(clearButton, "Temizle", 320, Color.FromArgb(51, 65, 85));
        clearButton.Click += ClearButton_Click;

        Button saveButton = new Button();
        ConfigureButton(saveButton, "Kaydet", 466, Color.FromArgb(29, 78, 216));
        saveButton.Click += SaveButton_Click;

        Label scopeLabel = new Label();
        scopeLabel.Text = "Yalnızca bu pencere aktifken tuş olayları kaydedilir. Başlamak için Başlat'a tıklayın.";
        scopeLabel.ForeColor = Color.FromArgb(148, 163, 184);
        scopeLabel.SetBounds(28, 552, 824, 30);
        scopeLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        Controls.Add(scopeLabel);

        countLabel.Text = "Toplam tuş: 0";
        lastKeyLabel.Text = "Son tuş: —";
        UpdateRecordingStatus();
    }

    private void ConfigureButton(Button button, string text, int left, Color color)
    {
        button.Text = text;
        button.SetBounds(left, 494, 132, 42);
        button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        button.BackColor = color;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.UseVisualStyleBackColor = false;
        button.Cursor = Cursors.Hand;
        Controls.Add(button);
    }

    private void Form1_KeyDown(object? sender, KeyEventArgs e)
    {
        // Yalnızca kendi formumuz aktifken gelen olayları kabul ediyoruz.
        if (ActiveForm != this || !ContainsFocus)
        {
            return;
        }

        KeyRecord? record = logger.AddRecord(e.KeyCode.ToString());
        if (record == null)
        {
            return;
        }

        recordList.Items.Add(record);
        recordList.TopIndex = recordList.Items.Count - 1;
        countLabel.Text = "Toplam tuş: " + logger.Count;
        lastKeyLabel.Text = "Son tuş: " + record.KeyName;
    }

    private void StartButton_Click(object? sender, EventArgs e)
    {
        logger.Start();
        UpdateRecordingStatus();
        recordList.Focus();
    }

    private void StopButton_Click(object? sender, EventArgs e)
    {
        logger.Stop();
        UpdateRecordingStatus();
    }

    private void ClearButton_Click(object? sender, EventArgs e)
    {
        logger.Clear();
        recordList.Items.Clear();
        countLabel.Text = "Toplam tuş: 0";
        lastKeyLabel.Text = "Son tuş: —";
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        using SaveFileDialog dialog = new SaveFileDialog();
        dialog.Title = "Tuş kayıtlarını kaydet";
        dialog.Filter = "Metin dosyası (*.txt)|*.txt";
        dialog.DefaultExt = "txt";
        dialog.AddExtension = true;
        dialog.FileName = "KeyScope-kayitlar.txt";

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            logger.SaveToFile(dialog.FileName);
            MessageBox.Show(this, "Kayıtlar TXT dosyasına kaydedildi.", "KeyScope",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Dosya kaydedilemedi. Konumu ve yazma izinlerini kontrol edin.\n\n"
                + ex.Message, "Kaydetme hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateRecordingStatus()
    {
        if (logger.IsRecording)
        {
            statusLabel.Text = "●  Kayıt aktif";
            statusLabel.ForeColor = Color.FromArgb(74, 222, 128);
        }
        else
        {
            statusLabel.Text = "●  Kayıt durduruldu";
            statusLabel.ForeColor = Color.FromArgb(251, 113, 133);
        }

        startButton.Enabled = !logger.IsRecording;
        stopButton.Enabled = logger.IsRecording;
    }
}
