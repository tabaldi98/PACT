using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Commom
{
    public static class FormExtensions
    {
        public static bool IsNullOrEmpty(this TextBox textBox)
        {
            return string.IsNullOrEmpty(textBox.Text);
        }

        public static bool IsNullOrEmpty(this RichTextBox textBox)
        {
            return string.IsNullOrEmpty(textBox.Text);
        }
        
        public static void SetLoading(this Form form, bool loading)
        {
            form.Enabled = !loading;
            Cursor.Current = !loading ? Cursors.WaitCursor : Cursors.Default;
        }

        public static void SetLoading(this UserControl form, bool loading)
        {
            form.Enabled = !loading;
            Cursor.Current = !loading ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}
