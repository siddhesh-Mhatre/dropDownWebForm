using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CurdWithCrud
{
	public partial class CurdWebFrom : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadCountries();
			}	

		}

		private void LoadCountries()
		{
			string query = "SELECT CountryID, CountryName FROM Country";
			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					con.Open();
					ddlCountry.DataSource = cmd.ExecuteReader();
					ddlCountry.DataTextField = "CountryName";
					ddlCountry.DataValueField = "CountryID";
					ddlCountry.DataBind();
					con.Close();
				}
			}
			ddlCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
		}



		protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadCities(Convert.ToInt32(ddlCountry.SelectedValue));
		}

		private void LoadCities(int countryId)
		{
			string query = "SELECT CityID, CityName FROM City WHERE CountryID = @CountryID";
			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@CountryID", countryId);
					con.Open();
					ddlCity.DataSource = cmd.ExecuteReader();
					ddlCity.DataTextField = "CityName";
					ddlCity.DataValueField = "CityID";
					ddlCity.DataBind();
					con.Close();
				}
			}
			ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
		}


		protected void btnSubmit_Click(object sender, EventArgs e)
        {
			string userName = txtUserName.Text;
			string phoneNumber = txtPhoneNumber.Text;
			int countryId = Convert.ToInt32(ddlCountry.SelectedValue);
			int cityId = Convert.ToInt32(ddlCity.SelectedValue);

			string query = "INSERT INTO UserData (UserName, PhoneNumber, CityID, CountryID) VALUES (@UserName, @PhoneNumber, @CityID, @CountryID)";
			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@UserName", userName);
					cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
					cmd.Parameters.AddWithValue("@CityID", cityId);
					cmd.Parameters.AddWithValue("@CountryID", countryId);

					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();
				}
			}
		}
    }
}