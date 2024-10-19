using Ferramenta.Models;
using System.Data.SqlClient;
using System.Data;

namespace Ferramenta.DAO
{
	public class FabricanteDAO
	{
		private SqlParameter[] CriarParametros(FabricanteViewModel fabricante)
		{
			SqlParameter[] parametros = new SqlParameter[1];
			parametros[0] = new SqlParameter("nome", fabricante.nome);
			return parametros;
		}

		private FabricanteViewModel MontarModel(DataRow registro)
		{
			FabricanteViewModel fabricante = new FabricanteViewModel();

			fabricante.id = Convert.ToInt32(registro["id"]);
			fabricante.nome = Convert.ToString(registro["nome"]);




			return fabricante;
		}


		public FabricanteViewModel Consulta(int id)
		{
			var p = new SqlParameter[]
			 {
				new SqlParameter("id", id)
			 };
			DataTable tabela = HelperDAO.ExecutaProcSelect("sp_consulta_fabricantes", p);

			if (tabela.Rows.Count == 0)
				return null;

			else
				return MontarModel(tabela.Rows[0]);
		}

		public List<FabricanteViewModel> Listagem()
		{
			List<FabricanteViewModel> lista = new List<FabricanteViewModel>();
			DataTable tabela = HelperDAO.ExecutaProcSelect("sp_lista_fabricantes", null);
			foreach (DataRow dr in tabela.Rows)
				lista.Add(MontarModel(dr));
			return lista;
		}

	}
}
