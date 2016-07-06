using System;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;

public class GridViewAdapter : ControlAdapter
{
    protected override void Render(HtmlTextWriter writer)
    {
        var grid = Control as GridView;
        if (null != grid) InitializeGridView(grid);

        base.Render(writer);
    }

    private void InitializeGridView(GridView grid)
    {
        grid.AllowPaging = false;
        grid.AllowSorting = false;

        //if (grid.HeaderRow != null)
        //{
        //    grid.HeaderRow.TableSection = TableRowSection.TableHeader;
        //}

        //foreach (GridViewRow row in grid.Rows)
        //{
        //    row.TableSection = TableRowSection.TableBody;
        //}

        //if (grid.FooterRow != null)
        //{
        //    grid.FooterRow.TableSection = TableRowSection.TableFooter;
        //}

        if (grid.Rows.Count <= 0) return;
        grid.UseAccessibleHeader = true;
        grid.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grid.ShowFooter)
            grid.FooterRow.TableSection = TableRowSection.TableFooter;
    }
}