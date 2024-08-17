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
	public partial class GridViewOpearation : System.Web.UI.Page
	{
		private string connectionString = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindGridView();
			}
		}

		private void BindGridView(string sortExpression = null)
		{
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", con))
				{
					con.Open();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);

					if (sortExpression != null)
					{
						DataView dv = dt.AsDataView();
						dv.Sort = sortExpression;
						GridView1.DataSource = dv;
					}
					else
					{
						GridView1.DataSource = dt;
					}

					GridView1.DataBind();
				}
			}
		}

		// Sorting
		protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
		{
			BindGridView(e.SortExpression);
		}

		// Paging
		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView1.PageIndex = e.NewPageIndex;
			BindGridView();
		}

		// Editing
		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView1.EditIndex = e.NewEditIndex;
			BindGridView();
		}

		// Updating
		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			int employeeID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
			string firstName = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
			string lastName = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
			string email = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
			string phoneNumber = ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
			string department = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

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

			GridView1.EditIndex = -1;
			BindGridView();
		}
		// Deleting
		protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			int employeeID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());

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

			BindGridView();
		}


		// Row Selection
		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int employeeID = Convert.ToInt32(GridView1.SelectedDataKey.Value);
			// You can now use employeeID to display more details or perform another action
		}

		// Export to Excel
		protected void btnExport_Click(object sender, EventArgs e)
		{
			GridView1.AllowPaging = false;
			BindGridView();

			Response.Clear();
			Response.Buffer = true;
			Response.AddHeader("content-disposition", "attachment;filename=EmployeeData.xls");
			Response.Charset = "";
			Response.ContentType = "application/vnd.ms-excel";
			StringWriter sw = new StringWriter();
			HtmlTextWriter hw = new HtmlTextWriter(sw);

			GridView1.RenderControl(hw);
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