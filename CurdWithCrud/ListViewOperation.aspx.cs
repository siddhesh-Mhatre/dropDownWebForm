using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CurdWithCrud
{
	public partial class ListViewOperation : System.Web.UI.Page
	{
		private string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindListView();
			}
		}


		private void BindListView()
		{
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", con))
				{
					con.Open();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);
					ListView1.DataSource = dt;
					ListView1.DataBind();
				}
			}
		}

		// Editing
		protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e)
		{
			ListView1.EditIndex = e.NewEditIndex;
			BindListView();
		}

		// Updating
		protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e)
		{
			int employeeID = Convert.ToInt32(ListView1.DataKeys[e.ItemIndex].Value);
			string firstName = ((TextBox)ListView1.Items[e.ItemIndex].FindControl("txtFirstName")).Text;
			string lastName = ((TextBox)ListView1.Items[e.ItemIndex].FindControl("txtLastName")).Text;
			string email = ((TextBox)ListView1.Items[e.ItemIndex].FindControl("txtEmail")).Text;
			string phoneNumber = ((TextBox)ListView1.Items[e.ItemIndex].FindControl("txtPhoneNumber")).Text;
			string department = ((TextBox)ListView1.Items[e.ItemIndex].FindControl("txtDepartment")).Text;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("UPDATE Employee SET FirstName=@FirstName, LastName=@LastName, Email=@Email, PhoneNumber=@PhoneNumber, Department=@Department WHERE EmployeeID=@EmployeeID", con))
				{
					cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
					cmd.Parameters.AddWithValue("@FirstName", firstName);
					cmd.Parameters.AddWithValue("@LastName", lastName);
					cmd.Parameters.AddWithValue("@Email", email);
					cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
					cmd.Parameters.AddWithValue("@Department", department);

					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();
				}
			}

			ListView1.EditIndex = -1;
			BindListView();
		}
		// Deleting
		protected void ListView1_ItemDeleting(object sender, ListViewDeleteEventArgs e)
		{
			int employeeID = Convert.ToInt32(ListView1.DataKeys[e.ItemIndex].Value);

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE EmployeeID=@EmployeeID", con))
				{
					cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();
				}
			}

			BindListView();
		}
		// Row Selection
		protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
		{
			if (e.CommandName == "Select")
			{
				int employeeID = Convert.ToInt32(ListView1.DataKeys[e.Item.DataItemIndex].Value);
				// You can now use employeeID to display more details or perform another action
			}
		}
		// Export to Excel
		protected void btnExport_Click(object sender, EventArgs e)
		{
			ListView1.DataBind();

			Response.Clear();
			Response.Buffer = true;
			Response.AddHeader("content-disposition", "attachment;filename=EmployeeData.xls");
			Response.Charset = "";
			Response.ContentType = "application/vnd.ms-excel";
			StringWriter sw = new StringWriter();
			HtmlTextWriter hw = new HtmlTextWriter(sw);

			ListView1.RenderControl(hw);
			Response.Output.Write(sw.ToString());
			Response.Flush();
			Response.End();
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
			// Required for exporting to Excel
		}

	}
}