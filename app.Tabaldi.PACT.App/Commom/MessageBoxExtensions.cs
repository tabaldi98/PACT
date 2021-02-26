using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Commom
{
    public static class MessageBoxExtensions
    {
        private static string _title = "Dr. Patricia Caroline";

        public static void ShowSucessMessage(string message)
        {
            MessageBox.Show(message, _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowDeleteQuestionMessage(string fieldName, IList<string> fields)
        {
            var sb = new StringBuilder();
            sb.Append($"Tem certeza que deseja deletar os seguintes {fieldName}:");
            foreach (var field in fields)
            {
                sb.Append("\n - " + field);
            }

           return MessageBox.Show(sb.ToString(), _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void ShowEmptyFieldsMessage(string field)
        {
            var sb = new StringBuilder();
            sb.Append("Preencha o seguinte campo:");
            sb.Append("\n - " + field);

            MessageBox.Show(sb.ToString(), _title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
