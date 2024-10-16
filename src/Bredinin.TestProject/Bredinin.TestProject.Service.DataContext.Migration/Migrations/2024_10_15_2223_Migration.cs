using FluentMigrator;

namespace Bredinin.TestProject.Service.DataContext.Migration.Migrations
{
    [Migration(2024_10_15_2223)]

    public class _2024_10_15_2223_Migration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(
                "CREATE TABLE public.products" +
                "(" +
                "id uuid NOT NULL," +
                "name varchar(255) NOT NULL," + 
                "description varchar(255) NULL," +
                "product_category_id uuid," + 
                "CONSTRAINT fk_products_product_category_id FOREIGN KEY (product_category_id) REFERENCES public.product_categories(id) ON DELETE CASCADE" +
                ");"
            );
        }
    }
}
