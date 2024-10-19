using System.Data.SqlClient;
using System.Data;
using Ferramenta.Models;

namespace Ferramenta.DAO
{
    public class FerramentaDAO
    {
        private SqlParameter[] CriarParametros(FerramentaViewModel ferramenta)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("descricao", ferramenta.descricao);
            parametros[1] = new SqlParameter("FabricanteId", ferramenta.fabricanteId);
            return parametros;
        }

        private FerramentaViewModel MontarModel(DataRow registro)
        {
            FerramentaViewModel ferramenta = new FerramentaViewModel();

            ferramenta.id = Convert.ToInt32(registro["id"]);
            ferramenta.descricao = Convert.ToString(registro["descricao"]);

            ferramenta.fabricanteId = Convert.ToInt32(registro["fabricanteId"]);

            FabricanteDAO dao = new FabricanteDAO();
            FabricanteViewModel categoria = dao.Consulta(ferramenta.fabricanteId);
            ferramenta.fabricanteNome = categoria.nome;



            return ferramenta;
        }


        public void Inserir(FerramentaViewModel jogo)
        {

            HelperDAO.ExecutaProc("sp_insert_ferramentas", CriarParametros(jogo));

        }

        public void Excluir(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id)

        };
            HelperDAO.ExecutaProc("sp_delete_ferrementas", p);

        }

    public void Alterar(FerramentaViewModel ferramenta)
    {
        var p = new SqlParameter[]
         {
           new SqlParameter("descricao", ferramenta.descricao),
            new SqlParameter("FabricanteId", ferramenta.fabricanteId),
            new SqlParameter("id", ferramenta.id)
         };

        HelperDAO.ExecutaProc("sp_update_ferramentas", p);
    }

    public FerramentaViewModel Consulta(int id)
    {
        var p = new SqlParameter[]
         {
                new SqlParameter("id", id)
         };
        DataTable tabela = HelperDAO.ExecutaProcSelect("sp_consulta_ferrementas", p);

        if (tabela.Rows.Count == 0)
            return null;

        else
            return MontarModel(tabela.Rows[0]);
    }

    public List<FerramentaViewModel> Listagem()
    {
        List<FerramentaViewModel> lista = new List<FerramentaViewModel>();
        DataTable tabela = HelperDAO.ExecutaProcSelect("sp_lista_ferramentas", null);
        foreach (DataRow dr in tabela.Rows)
            lista.Add(MontarModel(dr));
        return lista;
    }



}
}
