using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult CreateUser(string key, string name)
        {
            if (key != ConfigurationManager.AppSettings["SecretKey"])
            {
                return Content("Chave inválida.");
            }

            try
            {
                string accessToken = null;

                if (String.IsNullOrEmpty(name))
                {
                    return Content("Informe o nome.");
                }

                var existingUser = _context.Users.Where(u => u.Name == name).FirstOrDefault();

                if (existingUser != null)
                {
                    return Content("Já existe um usuário com este nome.");
                }

                while (true)
                {
                    accessToken = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

                    if (!_context.Users.Any(u => u.AccessToken == accessToken))
                        break;
                }

                var user = new User()
                {
                    AccessToken = accessToken,
                    Name = name
                };

                _context.Users.Add(user);

                if (_context.SaveChanges() > 0)
                    return Content(accessToken);
                else
                    return Content(":(");
            }
            catch (Exception ex)
            {
                return Content(String.Format("<pre>{0}</pre>", ex), "text/html");
            }
        }
        public ActionResult Seed()
        {
            try
            {
                SeedAcceleratorsQuestionnaire();
                SeedCollegeQuestionnaire();
                SeedCoworkingsQuestionnaire();
                SeedIncubatorQuestionnaire();
                SeedIndependentStartupGroupQuestionnaire();
                SeedInvestorssQuestionnaire();
                SeedStartupQuestionnaire();
                SeedVendorsQuestionnaire();

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
            if (_context.Questionnaires.Any(q => q.Alias == "accelerator"))
                return;

            var questionnaire = new Questionnaire()
            {
                Alias = "accelerator",
                Description = null,
                Name = "Aceleradora",
            };

            #region Dados Iniciais

            var group1 = new QuestionGroup()
            {
                SortOrder = 1,
                Name = "Dados da Aceleradora",
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
                FieldType = FormQuestionFieldType.EmailAddress,
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
                Title = "Quantas startups aceleradas?",
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 9,
                Title = "Quais? (opcional)",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = false
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 10,
                Title = "Quantas em aceleração?",
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = true
            });

            #endregion

            _context.Questionnaires.Add(questionnaire);
        }
        private void SeedCollegeQuestionnaire()
        {
            if (_context.Questionnaires.Any(q => q.Alias == "college"))
                return;

            var questionnaire = new Questionnaire()
            {
                Alias = "college",
                Description = null,
                Name = "Universidade",
            };

            #region Dados Iniciais

            var group1 = new QuestionGroup()
            {
                SortOrder = 1,
                Name = "Dados da Universidade",
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
                FieldType = FormQuestionFieldType.EmailAddress,
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
                Title = "Possui ações específicas para startups?",
                FieldType = FormQuestionFieldType.YesNo,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 8,
                Title = "Quais?",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = false
            });

            #endregion

            _context.Questionnaires.Add(questionnaire);
        }
        private void SeedCoworkingsQuestionnaire()
        {
            if (_context.Questionnaires.Any(q => q.Alias == "coworking"))
                return;

            var questionnaire = new Questionnaire()
            {
                Alias = "coworking",
                Description = null,
                Name = "Coworking",
            };

            #region Dados Iniciais

            var group1 = new QuestionGroup()
            {
                SortOrder = 1,
                Name = "Dados do Coworking",
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
                FieldType = FormQuestionFieldType.EmailAddress,
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
                Title = "Total de clientes",
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 9,
                Title = "Total de startups",
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = true
            });

            #endregion

            _context.Questionnaires.Add(questionnaire);
        }
        private void SeedIncubatorQuestionnaire()
        {
            if (_context.Questionnaires.Any(q => q.Alias == "incubator"))
                return;

            var questionnaire = new Questionnaire()
            {
                Alias = "incubator",
                Description = null,
                Name = "Incubadora",
            };

            #region Dados Iniciais

            var group1 = new QuestionGroup()
            {
                SortOrder = 1,
                Name = "Dados da Incubadora",
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
                FieldType = FormQuestionFieldType.EmailAddress,
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
                Title = "Quantas startups já foram incubadas?",
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 9,
                Title = "Quantas startups atualmente incubadas?",
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = false
            });

            #endregion

            _context.Questionnaires.Add(questionnaire);
        }
        private void SeedIndependentStartupGroupQuestionnaire()
        {
            if (_context.Questionnaires.Any(q => q.Alias == "independent-startup-group"))
                return;

            var questionnaire = new Questionnaire()
            {
                Alias = "independent-startup-group",
                Description = null,
                Name = "Grupo Autônomo de Startups",
            };

            #region Dados Iniciais

            var group1 = new QuestionGroup()
            {
                SortOrder = 1,
                Name = "Dados do Grupo Autônomo de Startups",
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
                FieldType = FormQuestionFieldType.EmailAddress,
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
                Title = "Quantos participantes?",
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 8,
                Title = "Quantos gestores ativos?",
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = true
            });

            #endregion

            _context.Questionnaires.Add(questionnaire);
        }
        private void SeedInvestorssQuestionnaire()
        {
            if (_context.Questionnaires.Any(q => q.Alias == "investor"))
                return;

            var questionnaire = new Questionnaire()
            {
                Alias = "investor",
                Description = null,
                Name = "Investidor",
            };

            #region Dados Iniciais

            var group1 = new QuestionGroup()
            {
                SortOrder = 1,
                Name = "Dados do Investidor",
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
                FieldType = FormQuestionFieldType.EmailAddress,
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
                Title = "Já investiu em startup?",
                FieldType = FormQuestionFieldType.YesNo,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 8,
                Title = "Quais? (opcional)",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            group1.Questions.Add(new FormQuestion()
            {
                SortOrder = 9,
                Title = "Valor disponível para investimento",
                FieldType = FormQuestionFieldType.Currency,
                IsMandatory = false
            });

            #endregion

            _context.Questionnaires.Add(questionnaire);
        }
        private void SeedStartupQuestionnaire()
        {
            if (_context.Questionnaires.Any(q => q.Alias == "startup"))
                return;

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
                Name = "Dados da Startup",
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
                SortOrder = 1,
                Title = "Site",
                FieldType = FormQuestionFieldType.Url,
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
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = false
            });

            group3.Questions.Add(new FormQuestion()
            {
                SortOrder = 2,
                Title = "Quantos projetos dos empreendedores obtiveram sucesso?",
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = false
            });

            group3.Questions.Add(new FormQuestion()
            {
                SortOrder = 3,
                Title = "Já tiveram alguma relação com investidores?",
                FieldType = FormQuestionFieldType.YesNo,
                IsMandatory = false
            });

            group3.Questions.Add(new FormQuestion()
            {
                SortOrder = 4,
                Title = "Quantos receberam investimento?",
                FieldType = FormQuestionFieldType.Integer,
                IsMandatory = false
            });

            #endregion

            _context.Questionnaires.Add(questionnaire);
        }
        private void SeedVendorsQuestionnaire()
        {
            if (_context.Questionnaires.Any(q => q.Alias == "vendor"))
                return;

            var questionnaire = new Questionnaire()
            {
                Alias = "vendor",
                Description = null,
                Name = "Fornecedor",
            };

            #region Dados Iniciais

            var group1 = new QuestionGroup()
            {
                SortOrder = 1,
                Name = "Dados do Fornecedor",
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
                FieldType = FormQuestionFieldType.EmailAddress,
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
                Title = "Área de atuação",
                FieldType = FormQuestionFieldType.Text,
                IsMandatory = true
            });

            #endregion

            _context.Questionnaires.Add(questionnaire);
        }
    }
}
