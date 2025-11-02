using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoolLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PageCount = table.Column<int>(type: "int", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "English"),
                    AvailableCopies = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalCopies = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.CheckConstraint("CK_Books_CopyCount", "[AvailableCopies] <= [TotalCopies]");
                    table.CheckConstraint("CK_Books_PositiveCopies", "[AvailableCopies] >= 0 AND [TotalCopies] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MembershipDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    MembershipStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MaxBooksAllowed = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.CheckConstraint("CK_Customers_MaxBooksAllowed", "[MaxBooksAllowed] > 0");
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    AuthorOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.BookId, x.AuthorId });
                    table.CheckConstraint("CK_BookAuthors_AuthorOrder", "[AuthorOrder] > 0");
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    LoanDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    RenewalCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LateFee = table.Column<decimal>(type: "decimal(8,2)", nullable: false, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.LoanId);
                    table.CheckConstraint("CK_Loans_LateFee", "[LateFee] >= 0");
                    table.CheckConstraint("CK_Loans_RenewalCount", "[RenewalCount] >= 0");
                    table.CheckConstraint("CK_Loans_ReturnDate", "[ReturnDate] IS NULL OR [ReturnDate] >= [LoanDate]");
                    table.ForeignKey(
                        name: "FK_Loans_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loans_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.CheckConstraint("CK_Reservations_ExpirationDate", "[ExpirationDate] >= [ReservationDate]");
                    table.ForeignKey(
                        name: "FK_Reservations_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.BookId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    FineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.FineId);
                    table.CheckConstraint("CK_Fines_Amount", "[Amount] > 0");
                    table.CheckConstraint("CK_Fines_PaidDate", "[PaidDate] IS NULL OR [PaidDate] >= [IssueDate]");
                    table.ForeignKey(
                        name: "FK_Fines_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fines_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "LoanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Biography", "BirthDate", "CreatedAt", "FirstName", "LastName", "Nationality", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "English novelist and essayist, journalist and critic", new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "George", "Orwell", "British", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "English novelist known primarily for her six major novels", new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Jane", "Austen", "British", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "British author, best known for the Harry Potter series", new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "J.K.", "Rowling", "British", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AvailableCopies", "CreatedAt", "Description", "ISBN", "Language", "PageCount", "PublicationDate", "Publisher", "Title", "TotalCopies", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A dystopian social science fiction novel and cautionary tale", "978-0-452-28423-4", "English", 328, new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Secker & Warburg", "1984", 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A romantic novel of manners", "978-0-14-143951-8", "English", 432, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "T. Egerton", "Pride and Prejudice", 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "The first novel in the Harry Potter series", "978-0-439-70818-8", "English", 223, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bloomsbury", "Harry Potter and the Philosopher's Stone", 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "An allegorical novella about farm animals", "978-0-452-28424-1", "English", 95, new DateTime(1945, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Secker & Warburg", "Animal Farm", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A novel about youthful hubris and romantic misunderstandings", "978-0-14-143952-5", "English", 474, new DateTime(1815, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Murray", "Emma", 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "City", "CreatedAt", "Email", "FirstName", "LastName", "MaxBooksAllowed", "MembershipDate", "MembershipStatus", "Phone", "PostalCode", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "New York", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "john.smith@email.com", "John", "Smith", 5, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "+1-555-0101", "10001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "456 Oak Avenue", "Los Angeles", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "emily.johnson@email.com", "Emily", "Johnson", 3, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "+1-555-0102", "90210", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Literary works of imaginative narration", "Fiction", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fiction involving magical or supernatural elements", "Fantasy", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Literature that has stood the test of time", "Classic Literature", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorId", "BookId", "AuthorOrder" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 1, 4, 1 },
                    { 2, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 3 },
                    { 3, 1 },
                    { 3, 2 },
                    { 4, 1 },
                    { 4, 3 },
                    { 5, 1 },
                    { 5, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Name",
                table: "Authors",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookId",
                table: "BookAuthors",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookId_AuthorOrder",
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_GenreId",
                table: "BookGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AvailableCopies",
                table: "Books",
                column: "AvailableCopies");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MembershipStatus",
                table: "Customers",
                column: "MembershipStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Name",
                table: "Customers",
                columns: new[] { "LastName", "FirstName" });

            migrationBuilder.CreateIndex(
                name: "IX_Fines_CustomerId",
                table: "Fines",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_LoanId",
                table: "Fines",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_Reason",
                table: "Fines",
                column: "Reason");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_Status",
                table: "Fines",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_Status_CustomerId",
                table: "Fines",
                columns: new[] { "Status", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CustomerId",
                table: "Loans",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_DueDate",
                table: "Loans",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_Status",
                table: "Loans",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_Status_DueDate",
                table: "Loans",
                columns: new[] { "Status", "DueDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BookId",
                table: "Reservations",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ExpirationDate",
                table: "Reservations",
                column: "ExpirationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Status",
                table: "Reservations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Status_ExpirationDate",
                table: "Reservations",
                columns: new[] { "Status", "ExpirationDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
