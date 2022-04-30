using ApiFuncionarios.Infra.Data.Entities;
using ApiFuncionarios.Infra.Data.Interfaces;
using ApiFuncionarios.Services.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFuncionarios.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        //atributo
        private readonly IFuncionarioRepository _funcionarioRepository;

        //construtor para injeção de dependência (inicialização)
        public FuncionariosController(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        /// <summary>
        /// Método da API para cadastro de funcionário
        /// </summary>
        [HttpPost]
        public IActionResult Post(FuncionarioPostRequest request)
        {
            try
            {
                #region Verificando se o CPF informado já existe no banco de dados

                if (_funcionarioRepository.ConsultarPorCpf(request.Cpf) != null)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { mensagem = "O CPF informado já está cadastrado no sistema." });

                #endregion

                #region Verificando se a Matrícula informada já existe no banco de dados

                if (_funcionarioRepository.ConsultarPorMatricula(request.Matricula) != null)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { mensagem = "A Matrícula informada já está cadastrada no sistema." });

                #endregion

                //capturando os dados do funcionário
                var funcionario = new Funcionario()
                {
                    IdFuncionario = Guid.NewGuid(),
                    Nome = request.Nome,
                    Cpf = request.Cpf,
                    Matricula = request.Matricula,
                    DataAdmissao = request.DataAdmissao
                };

                //cadastrando o funcionário
                _funcionarioRepository.Inserir(funcionario);

                //HTTP 201 (CREATED)
                return StatusCode(201, new { mensagem = "Funcionário cadastrado com sucesso", funcionario });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        /// <summary>
        /// Método da API para atualização de funcionário
        /// </summary>
        [HttpPut]
        public IActionResult Put(FuncionarioPutRequest request)
        {
            try
            {
                //consultar o funcionário no banco de dados através do id
                var funcionario = _funcionarioRepository.ObterPorId(request.IdFuncionario);

                #region Verificar se o funcionário não existe no banco de dados

                if (funcionario == null)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { mensagem = "Funcionário não encontrado." });

                #endregion

                #region Verificar se já existe outro funcionário com o mesmo CPF da edição

                var funcionarioPorCpf = _funcionarioRepository.ConsultarPorCpf(request.Cpf);
                if (funcionarioPorCpf != null && funcionarioPorCpf.IdFuncionario != funcionario.IdFuncionario)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { mensagem = "O CPF informado já está cadastrado para outro funcionário." });

                #endregion

                #region Verificar se já existe outro funcionário com a mesma Matrícula da edição

                var funcionarioPorMatricula = _funcionarioRepository.ConsultarPorMatricula(request.Matricula);
                if (funcionarioPorMatricula != null && funcionarioPorMatricula.IdFuncionario != funcionario.IdFuncionario)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { mensagem = "A Matrícula informada já está cadastrada para outro funcionário." });

                #endregion

                //capturando os dados do funcionário
                funcionario.Nome = request.Nome;
                funcionario.Cpf = request.Cpf;
                funcionario.Matricula = request.Matricula;
                funcionario.DataAdmissao = request.DataAdmissao;

                //atualizar os dados do funcionário
                _funcionarioRepository.Alterar(funcionario);

                //HTTP 200 (OK)
                return StatusCode(200, new { mensagem = "Funcionário atualizado com sucesso", funcionario });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        /// <summary>
        /// Método da API para exclusão de funcionário
        /// </summary>
        [HttpDelete("{idFuncionario}")]
        public IActionResult Delete(Guid idFuncionario)
        {
            try
            {
                //consultar o funcionário no banco de dados através do id
                var funcionario = _funcionarioRepository.ObterPorId(idFuncionario);

                #region Verificar se o funcionário não existe no banco de dados

                if (funcionario == null)
                    //HTTP 422 (UNPROCESSABLE ENTITY)
                    return StatusCode(422, new { mensagem = "Funcionário não encontrado." });

                #endregion

                //excluindoo funcionário
                _funcionarioRepository.Excluir(funcionario);

                //HTTP 200 (OK)
                return StatusCode(200, new { mensagem = "Funcionário excluído com sucesso", funcionario });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        /// <summary>
        /// Método da API para consulta de funcionários 
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                //consultar todos os funcionários cadastrados
                var funcionarios = _funcionarioRepository.Consultar();

                //HTTP 200 (OK)
                return StatusCode(200, funcionarios);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        /// <summary>
        /// Método para API para consultar de 1 funcionário baseado no ID
        /// </summary>
        [HttpGet("{idFuncionario}")]
        public IActionResult GetById(Guid idFuncionario)
        {
            try
            {
                //consultar 1 funcionário baseado no ID..
                var funcionario = _funcionarioRepository.ObterPorId(idFuncionario);

                //HTTP 200 (OK)
                return StatusCode(200, funcionario);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}




