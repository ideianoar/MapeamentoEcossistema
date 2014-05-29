using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapeamentoEcossistema.WebUI.Models;

namespace MapeamentoEcossistema.WebUI.Controllers
{
    public class ToolsController : Controller
    {
        // Fields.
        private readonly MapeamentoEntities _context;

        // Constructors.
        public ToolsController(MapeamentoEntities context)
        {
            _context = context;
        }

        // Actions.
        public ActionResult Seed()
        {
            try
            {
                SeedStartupQuestionnaire();
                SeedAcceleratorsQuestionnaire();

                if (_context.SaveChanges() > 0)
                {
                    return Content(":)");
                }
                else
                {
                    return Content(":(");
                }
            }
            catch (Exception ex)
            {
                return Content(String.Format("<pre>{0}</pre>", ex), "text/html");
            }
        }

        // Seed methods.
        private void SeedAcceleratorsQuestionnaire()
        {
            var questionnaire = new Questionnaire()
            {
                Alias = "accelerator",
                Description = null,
                Name = "Aceleradoras",
            };

            #region Dados Iniciais

            var group1 = new QuestionGroup()
            {
                SortOrder = 1,
                Name = "Dados Iniciais",
                Description = null
            };

            questionnaire.QuestionGroups.Add(group1);

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 1,
                Title = "Nome",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 2,
                Title = "Telefone",
                FieldType = FormQuestionFieldType.PhoneNumber,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 3,
                Title = "Cidade",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 4,
                Title = "Endereço",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 5,
                Title = "E-mail",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 6,
                Title = "Nome do contato",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 7,
                Title = "Tempo de existência",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 8,
                Title = "Quantidade de aceleradoras",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 9,
                Title = "Quais (opcional)?",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = false
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 10,
                Title = "Quantas em aceleração?",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = false
            });

            #endregion

            _context.Questionnaires.Add(questionnaire);
        }
        private void SeedStartupQuestionnaire()
        {
            var questionnaire = new Questionnaire()
            {
                Alias = "startup",
                Description = null,
                Name = "Startup",
            };

            #region Dados Iniciais

            var group1 = new QuestionGroup()
            {
                SortOrder = 1,
                Name = "Dados Iniciais",
                Description = null
            };

            questionnaire.QuestionGroups.Add(group1);

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 1,
                Title = "Nome da startup",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 2,
                Title = "Cidade",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 3,
                Title = "Endereço",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 4,
                Title = "E-mail",
                FieldType = FormQuestionFieldType.EmailAddress,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 5,
                Title = "Telefone",
                FieldType = FormQuestionFieldType.PhoneNumber,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 6,
                Title = "Nome do contato",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            #endregion

            #region Checkpoints Startup

            var group2 = new QuestionGroup()
            {
                SortOrder = 2,
                Name = "Checkpoints Startup",
                Description = null
            };

            questionnaire.QuestionGroups.Add(group2);

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 1,
                Title = "Definição do problema a ser resolvido",
                PrimaryPlaceholderText = "Qual é o problema?",
                SecondaryPlaceholderText = "Quando?",
                ExampleText = "Caso WAZE.",
                ProblemText = "Saber qual o melhor trajeto no trânsito naquele momento."
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 2,
                Title = "Validação do problema",
                PrimaryPlaceholderText = "Como foi feita?",
                SecondaryPlaceholderText = "Quando?",
                ExampleText = "Pesquisa com potenciais usuários",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 3,
                Title = "Desenho do modelo de negócio",
                PrimaryPlaceholderText = "Qual ferramenta foi usada?",
                SecondaryPlaceholderText = null,
                ExampleText = "Canvas.",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 4,
                Title = "Protótipo da solução",
                PrimaryPlaceholderText = "Comentário",
                SecondaryPlaceholderText = null,
                ExampleText = "Pode ser uma landing page.",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 5,
                Title = "Validação da solução",
                PrimaryPlaceholderText = "Como foi feita?",
                SecondaryPlaceholderText = "Quando?",
                ExampleText = "Teste do protótipo com o usuário.",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 6,
                Title = "MVP",
                PrimaryPlaceholderText = "Comentário",
                SecondaryPlaceholderText = "Quando?",
                ExampleText = "Buscar na internet (já pode ser vendido, sem tração de escala)",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 7,
                Title = "Formalização do negócio",
                PrimaryPlaceholderText = "Qual é o enquadramento?",
                SecondaryPlaceholderText = "Quando?",
                ExampleText = "MEI.",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 8,
                Title = "Primeira venda da solução",
                PrimaryPlaceholderText = "Descrição (pode ser de outra forma que não monetária)",
                SecondaryPlaceholderText = "Quando?",
                ExampleText = "Primeiro usuário cadastrado do mercado (que não tenha nenhum vínculo com os empreendedores).",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 9,
                Title = "Definição das métricas de startup AARRR (Leads, Mercado)",
                PrimaryPlaceholderText = "Quais?",
                SecondaryPlaceholderText = "Desde quando?",
                ExampleText = "Número de usuários cadastrados, Receita Financeira, Volume de vendas (modelo AARRR).",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 10,
                Title = "Definição das métricas de gestão",
                PrimaryPlaceholderText = "Quais?",
                SecondaryPlaceholderText = "Desde quando?",
                ExampleText = "Turn Over, Indicador de produtividade, Número de horas por atividade, etc.",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 11,
                Title = "Implantação dos processos de gestão",
                PrimaryPlaceholderText = "Quais?",
                SecondaryPlaceholderText = "Desde quando?",
                ExampleText = "Planilhas, app, sistema",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 12,
                Title = "Obtenção de tração inicial",
                PrimaryPlaceholderText = "Descrição",
                SecondaryPlaceholderText = "Quando ocorreu a primeira tração?",
                ExampleText = "Evolução em progressão geométrica de escala (passou de 10 para 100 para 1000 usuarios).",
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 13,
                Title = "Entrada numa aceleradora",
                PrimaryPlaceholderText = "Qual?",
                SecondaryPlaceholderText = "Quando?",
                ExampleText = null,
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 14,
                Title = "(CANAL PARALELO) Recebimento da proposta de investimento (contrato de confidencialidade e term sheet)",
                PrimaryPlaceholderText = "Descrição (opcional)",
                SecondaryPlaceholderText = "Quando?",
                ExampleText = null,
                ProblemText = null
            });

            group2.Questions.Add(new CheckpointQuestion()
            {
                SortOrder = 15,
                Title = "(CANAL PARALELO) Recebimento do investimento",
                PrimaryPlaceholderText = "Descrição (opcional) e valor estimado (opcional recomendado)",
                SecondaryPlaceholderText = "Quando?",
                ExampleText = null,
                ProblemText = null
            });

            #endregion

            #region Perfil empreendedor

            var group3 = new QuestionGroup()
            {
                SortOrder = 3,
                Name = "Perfil Empreendedor (opcional)",
                Description = null
            };

            questionnaire.QuestionGroups.Add(group3);

            group3.Questions.Add(new FormQuestion()
            {
                SortOrder = 1,
                Title = "Em média quantos projetos de startups os fundadores já participaram (excluindo esta)?",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = false
            });

            group3.Questions.Add(new FormQuestion()
            {
                SortOrder = 2,
                Title = "Quantos projetos dos empreendedores obtiveram sucesso?",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = false
            });

            group3.Questions.Add(new FormQuestion()
            {
                SortOrder = 3,
                Title = "Já tiveram alguma relação com investidores?",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = false
            });

            group3.Questions.Add(new FormQuestion()
            {
                SortOrder = 4,
                Title = "Quantos receberam investimento?",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = false
            });

            #endregion

            _context.Questionnaires.Add(questionnaire);
        }
    }
}
