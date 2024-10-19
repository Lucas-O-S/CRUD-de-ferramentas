using Ferramenta.DAO;
using Ferramenta.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ferramenta.Controllers
{
	public class FerramentaController : Controller
	{
		public IActionResult Index()
		{
			try
			{
				FerramentaDAO dao = new FerramentaDAO();
				List<FerramentaViewModel> lista = dao.Listagem();
				return View(lista);
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));

			}


		}

		public IActionResult Create()
		{
			try
			{
				ViewBag.operacao = "I";

				FerramentaViewModel ferramenta = new FerramentaViewModel();

				FerramentaDAO dao = new FerramentaDAO();

				FabricanteDAO fabDAO = new FabricanteDAO();
				ViewBag.fabricante = fabDAO.Listagem();


				return View("Form", ferramenta);
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));

			}

		}

		public IActionResult Salvar(FerramentaViewModel ferramenta, string operacao)
		{
			try
			{
				ValidarDados(ferramenta, operacao);
				if (ModelState.IsValid == false)
				{
					ViewBag.operacao = operacao;

					FabricanteDAO catDAO = new FabricanteDAO();
					ViewBag.fabricante = catDAO.Listagem();


					return View("form", ferramenta);


				}
				else
				{
					FerramentaDAO dao = new FerramentaDAO();
					if (operacao == "I")
					{
						dao.Inserir(ferramenta);
					}
					else
						dao.Alterar(ferramenta);
					return RedirectToAction("index");

				}




			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));

			}

		}

		public IActionResult Edit(int id)
		{
			try
			{
				ViewBag.operacao = "A";

				FabricanteDAO catDAO = new FabricanteDAO();
				ViewBag.fabricante = catDAO.Listagem();

				FerramentaDAO dao = new FerramentaDAO();
				FerramentaViewModel ferramenta = dao.Consulta(id);
				if (ferramenta == null)
				{
					return RedirectToAction("Index");
				}
				else
					return View("Form", ferramenta);
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));
			}
		}

		public IActionResult Delete(int id)
		{
			try
			{
				FerramentaDAO dao = new FerramentaDAO();
				dao.Excluir(id);
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				return View("Error", new ErrorViewModel(ex.ToString()));
			}

		}

		private void ValidarDados(FerramentaViewModel ferramenta, string operacao)
		{
			ModelState.Clear();
			FerramentaDAO dao = new FerramentaDAO();

			if (String.IsNullOrEmpty(ferramenta.descricao))
			{
				ModelState.AddModelError("descricao", "Descrição está vazia");

			}

		

		}
	}
}
