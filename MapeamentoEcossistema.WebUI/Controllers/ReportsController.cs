using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapeamentoEcossistema.WebUI.Code;

namespace MapeamentoEcossistema.WebUI.Controllers
{
    public class ReportsController : Controller
    {
        public ActionResult DownloadAnswers(string key)
        {
            if (key != ConfigurationManager.AppSettings["SecretKey"])
            {
                return Content(":(");
            }

            var dt = GetData(@"
                SELECT
	                Questionnaires.Name AS 'Questionário',
	                QuestionGroups.Name AS 'Grupo',
	                Questions.Title AS 'Questão',
	                FormResponses.ResponseText AS 'Form: Resposta',
	                CASE WHEN CheckpointResponses.YesNoFlag = 1 THEN 'Sim' ELSE 'Não' END AS 'Checkpoint: Sim/Não',
	                CheckpointQuestions.PrimaryPlaceholderText AS 'Checkpoint: P1',
	                CheckpointResponses.PrimaryResponseText AS 'Checkpoint: R1',
	                CheckpointQuestions.SecondaryPlaceholderText AS 'Checkpoint: P2',
	                CheckpointResponses.SecondaryResponseText AS 'Checkpoint: R2',
	                Responses.SessionId AS 'Sessão',
	                Users.Name AS 'Usuário',
	                Responses.CreatedDateTime AS 'Data/Hora'
                FROM Responses
                INNER JOIN Questions ON Responses.QuestionId = Questions.Id
                INNER JOIN QuestionGroups ON Questions.QuestionGroupId = QuestionGroups.Id
                INNER JOIN Questionnaires ON QuestionGroups.QuestionnaireId = Questionnaires.Id
                LEFT JOIN FormResponses ON Responses.Id = FormResponses.Id
                LEFT JOIN CheckpointResponses ON Responses.Id = CheckpointResponses.Id
                LEFT JOIN FormQuestions ON Questions.Id = FormQuestions.Id
                LEFT JOIN CheckpointQuestions ON Questions.Id = CheckpointQuestions.Id
                INNER JOIN Users ON Responses.UserId = Users.Id
                ORDER BY
	                Responses.SessionId,
	                Questionnaires.Name,
	                QuestionGroups.SortOrder,
	                Questions.SortOrder");

            var exported = ExcelExporter.Export(dt);

            Response.AddHeader("Content-Disposition", String.Format("attachment; filename=\"Ecossistema - Respostas - {0:dd/MM/yyyy}.xlsx\"", DateTime.Today));
            return File(exported, "application/vnd.ms-excel");
        }
        public ActionResult DownloadUsers(string key)
        {
            if (key != ConfigurationManager.AppSettings["SecretKey"])
            {
                return Content(":(");
            }

            var dt = GetData(@"
                SELECT AccessToken, Name
                FROM Users
                ORDER BY Name");

            var exported = ExcelExporter.Export(dt);

            Response.AddHeader("Content-Disposition", String.Format("attachment; filename=\"Ecossistema - Usuarios - {0:dd/MM/yyyy}.xlsx\"", DateTime.Today));
            return File(exported, "application/vnd.ms-excel");
        }

        // Private methods.
        private void ConvertDateTimeZone(DataTable table)
        {
            foreach (DataColumn column in table.Columns)
            {
                if (column.DataType == typeof(DateTime))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var value = row[column];
                        if (value != DBNull.Value)
                        {
                            var dateValue = (DateTime)value;
                            row[column] = DateTimeZoneHelper.ConvertFromUtc(dateValue);
                        }
                    }
                }
            }
        }
        private DataTable GetData(string sqlCommand)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Mapeamento"].ConnectionString))
            using (var command = connection.CreateCommand())
            using (var dataAdapter = new SqlDataAdapter(command))
            {
                command.CommandText = sqlCommand;
                command.CommandType = CommandType.Text;

                try
                {
                    var dataTable = new DataTable();
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    ConvertDateTimeZone(dataTable);

                    return dataTable;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
