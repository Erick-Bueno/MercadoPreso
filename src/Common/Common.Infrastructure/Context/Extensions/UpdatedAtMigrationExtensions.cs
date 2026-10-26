using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Infrastructure.Context.Extensions;

public static class UpdatedAtMigrationExtensions
{
    extension(MigrationBuilder migrationBuilder)
    {
        public void CreateUpdatedAtFunction(string schema) =>
            migrationBuilder.Sql(
                $"""
                CREATE OR REPLACE FUNCTION "{schema}".set_updated_at()
                RETURNS trigger AS $$
                BEGIN
                    NEW."UpdatedAt" = now();
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;
                """
            );

        public void AddUpdatedAtTriggers(string schema, params string[] tables)
        {
            foreach (var table in tables)
                migrationBuilder.Sql(
                    $"""
                    CREATE TRIGGER "trg_{table}_UpdatedAt"
                    BEFORE UPDATE ON "{schema}"."{table}"
                    FOR EACH ROW
                    EXECUTE FUNCTION "{schema}".set_updated_at();
                    """
                );
        }

        public void DropUpdatedAtTriggers(
            string schema,
            params string[] tables
        )
        {
            foreach (var table in tables)
                migrationBuilder.Sql(
                    $"""DROP TRIGGER IF EXISTS "trg_{table}_UpdatedAt" ON "{schema}"."{table}";"""
                );
        }

        public void DropUpdatedAtFunction(string schema) =>
            migrationBuilder.Sql($"""DROP FUNCTION IF EXISTS "{schema}".set_updated_at();""");
    }
}
