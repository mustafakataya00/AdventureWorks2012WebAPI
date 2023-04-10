using System;
using System.Collections.Generic;
using AdventureWorkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorkAPI.Data;

public partial class AdventureWorks2012Context : DbContext
{
    public AdventureWorks2012Context()
    {
    }

    public AdventureWorks2012Context(DbContextOptions<AdventureWorks2012Context> options)
        : base(options)
    {
    }

    public virtual DbSet<BillOfMaterial> BillOfMaterials { get; set; }

    public virtual DbSet<Culture> Cultures { get; set; }

    public virtual DbSet<Illustration> Illustrations { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductCostHistory> ProductCostHistories { get; set; }

    public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }

    public virtual DbSet<ProductInventory> ProductInventories { get; set; }

    public virtual DbSet<ProductListPriceHistory> ProductListPriceHistories { get; set; }

    public virtual DbSet<ProductModel> ProductModels { get; set; }

    public virtual DbSet<ProductModelIllustration> ProductModelIllustrations { get; set; }

    public virtual DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; }

    public virtual DbSet<ProductPhoto> ProductPhotos { get; set; }

    public virtual DbSet<ProductProductPhoto> ProductProductPhotos { get; set; }

    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    public virtual DbSet<ProductSubcategory> ProductSubcategories { get; set; }

    public virtual DbSet<ScrapReason> ScrapReasons { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public virtual DbSet<TransactionHistoryArchive> TransactionHistoryArchives { get; set; }

    public virtual DbSet<UnitMeasure> UnitMeasures { get; set; }

    public virtual DbSet<VProductAndDescription> VProductAndDescriptions { get; set; }

    public virtual DbSet<VProductModelCatalogDescription> VProductModelCatalogDescriptions { get; set; }

    public virtual DbSet<VProductModelInstruction> VProductModelInstructions { get; set; }

    public virtual DbSet<WorkOrder> WorkOrders { get; set; }

    public virtual DbSet<WorkOrderRouting> WorkOrderRoutings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<BillOfMaterial>(entity =>
        {
            entity.HasKey(e => e.BillOfMaterialsId)
                .HasName("PK_BillOfMaterials_BillOfMaterialsID")
                .IsClustered(false);

            entity.ToTable("BillOfMaterials", "Production", tb => tb.HasComment("Items required to make bicycles and bicycle subassemblies. It identifies the heirarchical relationship between a parent product and its components."));

            entity.HasIndex(e => new { e.ProductAssemblyId, e.ComponentId, e.StartDate }, "AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => e.UnitMeasureCode, "IX_BillOfMaterials_UnitMeasureCode");

            entity.Property(e => e.BillOfMaterialsId)
                .HasComment("Primary key for BillOfMaterials records.")
                .HasColumnName("BillOfMaterialsID");
            entity.Property(e => e.Bomlevel)
                .HasComment("Indicates the depth the component is from its parent (AssemblyID).")
                .HasColumnName("BOMLevel");
            entity.Property(e => e.ComponentId)
                .HasComment("Component identification number. Foreign key to Product.ProductID.")
                .HasColumnName("ComponentID");
            entity.Property(e => e.EndDate)
                .HasComment("Date the component stopped being used in the assembly item.")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.PerAssemblyQty)
                .HasDefaultValueSql("((1.00))")
                .HasComment("Quantity of the component needed to create the assembly.")
                .HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ProductAssemblyId)
                .HasComment("Parent product identification number. Foreign key to Product.ProductID.")
                .HasColumnName("ProductAssemblyID");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date the component started being used in the assembly item.")
                .HasColumnType("datetime");
            entity.Property(e => e.UnitMeasureCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Standard code identifying the unit of measure for the quantity.");

            entity.HasOne(d => d.Component).WithMany(p => p.BillOfMaterialComponents)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductAssembly).WithMany(p => p.BillOfMaterialProductAssemblies).HasForeignKey(d => d.ProductAssemblyId);

            entity.HasOne(d => d.UnitMeasureCodeNavigation).WithMany(p => p.BillOfMaterials)
                .HasForeignKey(d => d.UnitMeasureCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Culture>(entity =>
        {
            entity.HasKey(e => e.CultureId).HasName("PK_Culture_CultureID");

            entity.ToTable("Culture", "Production", tb => tb.HasComment("Lookup table containing the languages in which some AdventureWorks data is stored."));

            entity.HasIndex(e => e.Name, "AK_Culture_Name").IsUnique();

            entity.Property(e => e.CultureId)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasComment("Primary key for Culture records.")
                .HasColumnName("CultureID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Culture description.");
        });

        modelBuilder.Entity<Illustration>(entity =>
        {
            entity.HasKey(e => e.IllustrationId).HasName("PK_Illustration_IllustrationID");

            entity.ToTable("Illustration", "Production", tb => tb.HasComment("Bicycle assembly diagrams."));

            entity.Property(e => e.IllustrationId)
                .HasComment("Primary key for Illustration records.")
                .HasColumnName("IllustrationID");
            entity.Property(e => e.Diagram)
                .HasComment("Illustrations used in manufacturing instructions. Stored as XML.")
                .HasColumnType("xml");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK_Location_LocationID");

            entity.ToTable("Location", "Production", tb => tb.HasComment("Product inventory and manufacturing locations."));

            entity.HasIndex(e => e.Name, "AK_Location_Name").IsUnique();

            entity.Property(e => e.LocationId)
                .HasComment("Primary key for Location records.")
                .HasColumnName("LocationID");
            entity.Property(e => e.Availability)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Work capacity (in hours) of the manufacturing location.")
                .HasColumnType("decimal(8, 2)");
            entity.Property(e => e.CostRate)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Standard hourly cost of the manufacturing location.")
                .HasColumnType("smallmoney");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Location description.");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_Product_ProductID");

            entity.ToTable("Product", "Production", tb => tb.HasComment("Products sold or used in the manfacturing of sold products."));

            entity.HasIndex(e => e.Name, "AK_Product_Name").IsUnique();

            entity.HasIndex(e => e.ProductNumber, "AK_Product_ProductNumber").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_Product_rowguid").IsUnique();

            entity.Property(e => e.ProductId)
                .HasComment("Primary key for Product records.")
                .HasColumnName("ProductID");
            entity.Property(e => e.Class)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasComment("H = High, M = Medium, L = Low");
            entity.Property(e => e.Color)
                .HasMaxLength(15)
                .HasComment("Product color.");
            entity.Property(e => e.DaysToManufacture).HasComment("Number of days required to manufacture the product.");
            entity.Property(e => e.DiscontinuedDate)
                .HasComment("Date the product was discontinued.")
                .HasColumnType("datetime");
            entity.Property(e => e.FinishedGoodsFlag)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Product is not a salable item. 1 = Product is salable.");
            entity.Property(e => e.ListPrice)
                .HasComment("Selling price.")
                .HasColumnType("money");
            entity.Property(e => e.MakeFlag)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Product is purchased, 1 = Product is manufactured in-house.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Name of the product.");
            entity.Property(e => e.ProductLine)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasComment("R = Road, M = Mountain, T = Touring, S = Standard");
            entity.Property(e => e.ProductModelId)
                .HasComment("Product is a member of this product model. Foreign key to ProductModel.ProductModelID.")
                .HasColumnName("ProductModelID");
            entity.Property(e => e.ProductNumber)
                .HasMaxLength(25)
                .HasComment("Unique product identification number.");
            entity.Property(e => e.ProductSubcategoryId)
                .HasComment("Product is a member of this product subcategory. Foreign key to ProductSubCategory.ProductSubCategoryID. ")
                .HasColumnName("ProductSubcategoryID");
            entity.Property(e => e.ReorderPoint).HasComment("Inventory level that triggers a purchase order or work order. ");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
            entity.Property(e => e.SafetyStockLevel).HasComment("Minimum inventory quantity. ");
            entity.Property(e => e.SellEndDate)
                .HasComment("Date the product was no longer available for sale.")
                .HasColumnType("datetime");
            entity.Property(e => e.SellStartDate)
                .HasComment("Date the product was available for sale.")
                .HasColumnType("datetime");
            entity.Property(e => e.Size)
                .HasMaxLength(5)
                .HasComment("Product size.");
            entity.Property(e => e.SizeUnitMeasureCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Unit of measure for Size column.");
            entity.Property(e => e.StandardCost)
                .HasComment("Standard cost of the product.")
                .HasColumnType("money");
            entity.Property(e => e.Style)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasComment("W = Womens, M = Mens, U = Universal");
            entity.Property(e => e.Weight)
                .HasComment("Product weight.")
                .HasColumnType("decimal(8, 2)");
            entity.Property(e => e.WeightUnitMeasureCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Unit of measure for Weight column.");

            entity.HasOne(d => d.ProductModel).WithMany(p => p.Products).HasForeignKey(d => d.ProductModelId);

            entity.HasOne(d => d.ProductSubcategory).WithMany(p => p.Products).HasForeignKey(d => d.ProductSubcategoryId);

            entity.HasOne(d => d.SizeUnitMeasureCodeNavigation).WithMany(p => p.ProductSizeUnitMeasureCodeNavigations).HasForeignKey(d => d.SizeUnitMeasureCode);

            entity.HasOne(d => d.WeightUnitMeasureCodeNavigation).WithMany(p => p.ProductWeightUnitMeasureCodeNavigations).HasForeignKey(d => d.WeightUnitMeasureCode);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK_ProductCategory_ProductCategoryID");

            entity.ToTable("ProductCategory", "Production", tb => tb.HasComment("High-level product categorization."));

            entity.HasIndex(e => e.Name, "AK_ProductCategory_Name").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_ProductCategory_rowguid").IsUnique();

            entity.Property(e => e.ProductCategoryId)
                .HasComment("Primary key for ProductCategory records.")
                .HasColumnName("ProductCategoryID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Category description.");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductCostHistory>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.StartDate }).HasName("PK_ProductCostHistory_ProductID_StartDate");

            entity.ToTable("ProductCostHistory", "Production", tb => tb.HasComment("Changes in the cost of a product over time."));

            entity.Property(e => e.ProductId)
                .HasComment("Product identification number. Foreign key to Product.ProductID")
                .HasColumnName("ProductID");
            entity.Property(e => e.StartDate)
                .HasComment("Product cost start date.")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate)
                .HasComment("Product cost end date.")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.StandardCost)
                .HasComment("Standard cost of the product.")
                .HasColumnType("money");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCostHistories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductDescription>(entity =>
        {
            entity.HasKey(e => e.ProductDescriptionId).HasName("PK_ProductDescription_ProductDescriptionID");

            entity.ToTable("ProductDescription", "Production", tb => tb.HasComment("Product descriptions in several languages."));

            entity.HasIndex(e => e.Rowguid, "AK_ProductDescription_rowguid").IsUnique();

            entity.Property(e => e.ProductDescriptionId)
                .HasComment("Primary key for ProductDescription records.")
                .HasColumnName("ProductDescriptionID");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasComment("Description of the product.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductInventory>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.LocationId }).HasName("PK_ProductInventory_ProductID_LocationID");

            entity.ToTable("ProductInventory", "Production", tb => tb.HasComment("Product inventory information."));

            entity.Property(e => e.ProductId)
                .HasComment("Product identification number. Foreign key to Product.ProductID.")
                .HasColumnName("ProductID");
            entity.Property(e => e.LocationId)
                .HasComment("Inventory location identification number. Foreign key to Location.LocationID. ")
                .HasColumnName("LocationID");
            entity.Property(e => e.Bin).HasComment("Storage container on a shelf in an inventory location.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasComment("Quantity of products in the inventory location.");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
            entity.Property(e => e.Shelf)
                .HasMaxLength(10)
                .HasComment("Storage compartment within an inventory location.");

            entity.HasOne(d => d.Location).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductListPriceHistory>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.StartDate }).HasName("PK_ProductListPriceHistory_ProductID_StartDate");

            entity.ToTable("ProductListPriceHistory", "Production", tb => tb.HasComment("Changes in the list price of a product over time."));

            entity.Property(e => e.ProductId)
                .HasComment("Product identification number. Foreign key to Product.ProductID")
                .HasColumnName("ProductID");
            entity.Property(e => e.StartDate)
                .HasComment("List price start date.")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate)
                .HasComment("List price end date")
                .HasColumnType("datetime");
            entity.Property(e => e.ListPrice)
                .HasComment("Product list price.")
                .HasColumnType("money");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductListPriceHistories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.HasKey(e => e.ProductModelId).HasName("PK_ProductModel_ProductModelID");

            entity.ToTable("ProductModel", "Production", tb => tb.HasComment("Product model classification."));

            entity.HasIndex(e => e.Name, "AK_ProductModel_Name").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_ProductModel_rowguid").IsUnique();

            entity.HasIndex(e => e.CatalogDescription, "PXML_ProductModel_CatalogDescription");

            entity.HasIndex(e => e.Instructions, "PXML_ProductModel_Instructions");

            entity.Property(e => e.ProductModelId)
                .HasComment("Primary key for ProductModel records.")
                .HasColumnName("ProductModelID");
            entity.Property(e => e.CatalogDescription)
                .HasComment("Detailed product catalog information in xml format.")
                .HasColumnType("xml");
            entity.Property(e => e.Instructions)
                .HasComment("Manufacturing instructions in xml format.")
                .HasColumnType("xml");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Product model description.");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductModelIllustration>(entity =>
        {
            entity.HasKey(e => new { e.ProductModelId, e.IllustrationId }).HasName("PK_ProductModelIllustration_ProductModelID_IllustrationID");

            entity.ToTable("ProductModelIllustration", "Production", tb => tb.HasComment("Cross-reference table mapping product models and illustrations."));

            entity.Property(e => e.ProductModelId)
                .HasComment("Primary key. Foreign key to ProductModel.ProductModelID.")
                .HasColumnName("ProductModelID");
            entity.Property(e => e.IllustrationId)
                .HasComment("Primary key. Foreign key to Illustration.IllustrationID.")
                .HasColumnName("IllustrationID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Illustration).WithMany(p => p.ProductModelIllustrations)
                .HasForeignKey(d => d.IllustrationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductModel).WithMany(p => p.ProductModelIllustrations)
                .HasForeignKey(d => d.ProductModelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductModelProductDescriptionCulture>(entity =>
        {
            entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.CultureId }).HasName("PK_ProductModelProductDescriptionCulture_ProductModelID_ProductDescriptionID_CultureID");

            entity.ToTable("ProductModelProductDescriptionCulture", "Production", tb => tb.HasComment("Cross-reference table mapping product descriptions and the language the description is written in."));

            entity.Property(e => e.ProductModelId)
                .HasComment("Primary key. Foreign key to ProductModel.ProductModelID.")
                .HasColumnName("ProductModelID");
            entity.Property(e => e.ProductDescriptionId)
                .HasComment("Primary key. Foreign key to ProductDescription.ProductDescriptionID.")
                .HasColumnName("ProductDescriptionID");
            entity.Property(e => e.CultureId)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasComment("Culture identification number. Foreign key to Culture.CultureID.")
                .HasColumnName("CultureID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Culture).WithMany(p => p.ProductModelProductDescriptionCultures)
                .HasForeignKey(d => d.CultureId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductDescription).WithMany(p => p.ProductModelProductDescriptionCultures)
                .HasForeignKey(d => d.ProductDescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductModel).WithMany(p => p.ProductModelProductDescriptionCultures)
                .HasForeignKey(d => d.ProductModelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductPhoto>(entity =>
        {
            entity.HasKey(e => e.ProductPhotoId).HasName("PK_ProductPhoto_ProductPhotoID");

            entity.ToTable("ProductPhoto", "Production", tb => tb.HasComment("Product images."));

            entity.Property(e => e.ProductPhotoId)
                .HasComment("Primary key for ProductPhoto records.")
                .HasColumnName("ProductPhotoID");
            entity.Property(e => e.LargePhoto).HasComment("Large image of the product.");
            entity.Property(e => e.LargePhotoFileName)
                .HasMaxLength(50)
                .HasComment("Large image file name.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.ThumbNailPhoto).HasComment("Small image of the product.");
            entity.Property(e => e.ThumbnailPhotoFileName)
                .HasMaxLength(50)
                .HasComment("Small image file name.");
        });

        modelBuilder.Entity<ProductProductPhoto>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ProductPhotoId })
                .HasName("PK_ProductProductPhoto_ProductID_ProductPhotoID")
                .IsClustered(false);

            entity.ToTable("ProductProductPhoto", "Production", tb => tb.HasComment("Cross-reference table mapping products and product photos."));

            entity.Property(e => e.ProductId)
                .HasComment("Product identification number. Foreign key to Product.ProductID.")
                .HasColumnName("ProductID");
            entity.Property(e => e.ProductPhotoId)
                .HasComment("Product photo identification number. Foreign key to ProductPhoto.ProductPhotoID.")
                .HasColumnName("ProductPhotoID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Primary).HasComment("0 = Photo is not the principal image. 1 = Photo is the principal image.");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductProductPhotos)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductPhoto).WithMany(p => p.ProductProductPhotos)
                .HasForeignKey(d => d.ProductPhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.ProductReviewId).HasName("PK_ProductReview_ProductReviewID");

            entity.ToTable("ProductReview", "Production", tb => tb.HasComment("Customer reviews of products they have purchased."));

            entity.HasIndex(e => new { e.ProductId, e.ReviewerName }, "IX_ProductReview_ProductID_Name");

            entity.Property(e => e.ProductReviewId)
                .HasComment("Primary key for ProductReview records.")
                .HasColumnName("ProductReviewID");
            entity.Property(e => e.Comments)
                .HasMaxLength(3850)
                .HasComment("Reviewer's comments");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .HasComment("Reviewer's e-mail address.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId)
                .HasComment("Product identification number. Foreign key to Product.ProductID.")
                .HasColumnName("ProductID");
            entity.Property(e => e.Rating).HasComment("Product rating given by the reviewer. Scale is 1 to 5 with 5 as the highest rating.");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date review was submitted.")
                .HasColumnType("datetime");
            entity.Property(e => e.ReviewerName)
                .HasMaxLength(50)
                .HasComment("Name of the reviewer.");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ProductSubcategory>(entity =>
        {
            entity.HasKey(e => e.ProductSubcategoryId).HasName("PK_ProductSubcategory_ProductSubcategoryID");

            entity.ToTable("ProductSubcategory", "Production", tb => tb.HasComment("Product subcategories. See ProductCategory table."));

            entity.HasIndex(e => e.Name, "AK_ProductSubcategory_Name").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_ProductSubcategory_rowguid").IsUnique();

            entity.Property(e => e.ProductSubcategoryId)
                .HasComment("Primary key for ProductSubcategory records.")
                .HasColumnName("ProductSubcategoryID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Subcategory description.");
            entity.Property(e => e.ProductCategoryId)
                .HasComment("Product category identification number. Foreign key to ProductCategory.ProductCategoryID.")
                .HasColumnName("ProductCategoryID");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.ProductSubcategories)
                .HasForeignKey(d => d.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ScrapReason>(entity =>
        {
            entity.HasKey(e => e.ScrapReasonId).HasName("PK_ScrapReason_ScrapReasonID");

            entity.ToTable("ScrapReason", "Production", tb => tb.HasComment("Manufacturing failure reasons lookup table."));

            entity.HasIndex(e => e.Name, "AK_ScrapReason_Name").IsUnique();

            entity.Property(e => e.ScrapReasonId)
                .HasComment("Primary key for ScrapReason records.")
                .HasColumnName("ScrapReasonID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Failure description.");
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK_TransactionHistory_TransactionID");

            entity.ToTable("TransactionHistory", "Production", tb => tb.HasComment("Record of each purchase order, sales order, or work order transaction year to date."));

            entity.HasIndex(e => e.ProductId, "IX_TransactionHistory_ProductID");

            entity.HasIndex(e => new { e.ReferenceOrderId, e.ReferenceOrderLineId }, "IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID");

            entity.Property(e => e.TransactionId)
                .HasComment("Primary key for TransactionHistory records.")
                .HasColumnName("TransactionID");
            entity.Property(e => e.ActualCost)
                .HasComment("Product cost.")
                .HasColumnType("money");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId)
                .HasComment("Product identification number. Foreign key to Product.ProductID.")
                .HasColumnName("ProductID");
            entity.Property(e => e.Quantity).HasComment("Product quantity.");
            entity.Property(e => e.ReferenceOrderId)
                .HasComment("Purchase order, sales order, or work order identification number.")
                .HasColumnName("ReferenceOrderID");
            entity.Property(e => e.ReferenceOrderLineId)
                .HasComment("Line number associated with the purchase order, sales order, or work order.")
                .HasColumnName("ReferenceOrderLineID");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time of the transaction.")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasComment("W = WorkOrder, S = SalesOrder, P = PurchaseOrder");

            entity.HasOne(d => d.Product).WithMany(p => p.TransactionHistories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TransactionHistoryArchive>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK_TransactionHistoryArchive_TransactionID");

            entity.ToTable("TransactionHistoryArchive", "Production", tb => tb.HasComment("Transactions for previous years."));

            entity.HasIndex(e => e.ProductId, "IX_TransactionHistoryArchive_ProductID");

            entity.HasIndex(e => new { e.ReferenceOrderId, e.ReferenceOrderLineId }, "IX_TransactionHistoryArchive_ReferenceOrderID_ReferenceOrderLineID");

            entity.Property(e => e.TransactionId)
                .ValueGeneratedNever()
                .HasComment("Primary key for TransactionHistoryArchive records.")
                .HasColumnName("TransactionID");
            entity.Property(e => e.ActualCost)
                .HasComment("Product cost.")
                .HasColumnType("money");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId)
                .HasComment("Product identification number. Foreign key to Product.ProductID.")
                .HasColumnName("ProductID");
            entity.Property(e => e.Quantity).HasComment("Product quantity.");
            entity.Property(e => e.ReferenceOrderId)
                .HasComment("Purchase order, sales order, or work order identification number.")
                .HasColumnName("ReferenceOrderID");
            entity.Property(e => e.ReferenceOrderLineId)
                .HasComment("Line number associated with the purchase order, sales order, or work order.")
                .HasColumnName("ReferenceOrderLineID");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time of the transaction.")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasComment("W = Work Order, S = Sales Order, P = Purchase Order");
        });

        modelBuilder.Entity<UnitMeasure>(entity =>
        {
            entity.HasKey(e => e.UnitMeasureCode).HasName("PK_UnitMeasure_UnitMeasureCode");

            entity.ToTable("UnitMeasure", "Production", tb => tb.HasComment("Unit of measure lookup table."));

            entity.HasIndex(e => e.Name, "AK_UnitMeasure_Name").IsUnique();

            entity.Property(e => e.UnitMeasureCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Primary key.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Unit of measure description.");
        });

        modelBuilder.Entity<VProductAndDescription>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductAndDescription", "Production");

            entity.Property(e => e.CultureId)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("CultureID");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductModel).HasMaxLength(50);
        });

        modelBuilder.Entity<VProductModelCatalogDescription>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductModelCatalogDescription", "Production");

            entity.Property(e => e.Color).HasMaxLength(256);
            entity.Property(e => e.Copyright).HasMaxLength(30);
            entity.Property(e => e.Crankset).HasMaxLength(256);
            entity.Property(e => e.MaintenanceDescription).HasMaxLength(256);
            entity.Property(e => e.Material).HasMaxLength(256);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfYears).HasMaxLength(256);
            entity.Property(e => e.Pedal).HasMaxLength(256);
            entity.Property(e => e.PictureAngle).HasMaxLength(256);
            entity.Property(e => e.PictureSize).HasMaxLength(256);
            entity.Property(e => e.ProductLine).HasMaxLength(256);
            entity.Property(e => e.ProductModelId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProductModelID");
            entity.Property(e => e.ProductPhotoId)
                .HasMaxLength(256)
                .HasColumnName("ProductPhotoID");
            entity.Property(e => e.ProductUrl)
                .HasMaxLength(256)
                .HasColumnName("ProductURL");
            entity.Property(e => e.RiderExperience).HasMaxLength(1024);
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.Saddle).HasMaxLength(256);
            entity.Property(e => e.Style).HasMaxLength(256);
            entity.Property(e => e.WarrantyDescription).HasMaxLength(256);
            entity.Property(e => e.WarrantyPeriod).HasMaxLength(256);
            entity.Property(e => e.Wheel).HasMaxLength(256);
        });

        modelBuilder.Entity<VProductModelInstruction>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductModelInstructions", "Production");

            entity.Property(e => e.LaborHours).HasColumnType("decimal(9, 4)");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.MachineHours).HasColumnType("decimal(9, 4)");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ProductModelId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProductModelID");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.SetupHours).HasColumnType("decimal(9, 4)");
            entity.Property(e => e.Step).HasMaxLength(1024);
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.HasKey(e => e.WorkOrderId).HasName("PK_WorkOrder_WorkOrderID");

            entity.ToTable("WorkOrder", "Production", tb =>
                {
                    tb.HasComment("Manufacturing work orders.");
                    tb.HasTrigger("iWorkOrder");
                    tb.HasTrigger("uWorkOrder");
                });

            entity.HasIndex(e => e.ProductId, "IX_WorkOrder_ProductID");

            entity.HasIndex(e => e.ScrapReasonId, "IX_WorkOrder_ScrapReasonID");

            entity.Property(e => e.WorkOrderId)
                .HasComment("Primary key for WorkOrder records.")
                .HasColumnName("WorkOrderID");
            entity.Property(e => e.DueDate)
                .HasComment("Work order due date.")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate)
                .HasComment("Work order end date.")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderQty).HasComment("Product quantity to build.");
            entity.Property(e => e.ProductId)
                .HasComment("Product identification number. Foreign key to Product.ProductID.")
                .HasColumnName("ProductID");
            entity.Property(e => e.ScrapReasonId)
                .HasComment("Reason for inspection failure.")
                .HasColumnName("ScrapReasonID");
            entity.Property(e => e.ScrappedQty).HasComment("Quantity that failed inspection.");
            entity.Property(e => e.StartDate)
                .HasComment("Work order start date.")
                .HasColumnType("datetime");
            entity.Property(e => e.StockedQty)
                .HasComputedColumnSql("(isnull([OrderQty]-[ScrappedQty],(0)))", false)
                .HasComment("Quantity built and put in inventory.");

            entity.HasOne(d => d.Product).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ScrapReason).WithMany(p => p.WorkOrders).HasForeignKey(d => d.ScrapReasonId);
        });

        modelBuilder.Entity<WorkOrderRouting>(entity =>
        {
            entity.HasKey(e => new { e.WorkOrderId, e.ProductId, e.OperationSequence }).HasName("PK_WorkOrderRouting_WorkOrderID_ProductID_OperationSequence");

            entity.ToTable("WorkOrderRouting", "Production", tb => tb.HasComment("Work order details."));

            entity.HasIndex(e => e.ProductId, "IX_WorkOrderRouting_ProductID");

            entity.Property(e => e.WorkOrderId)
                .HasComment("Primary key. Foreign key to WorkOrder.WorkOrderID.")
                .HasColumnName("WorkOrderID");
            entity.Property(e => e.ProductId)
                .HasComment("Primary key. Foreign key to Product.ProductID.")
                .HasColumnName("ProductID");
            entity.Property(e => e.OperationSequence).HasComment("Primary key. Indicates the manufacturing process sequence.");
            entity.Property(e => e.ActualCost)
                .HasComment("Actual manufacturing cost.")
                .HasColumnType("money");
            entity.Property(e => e.ActualEndDate)
                .HasComment("Actual end date.")
                .HasColumnType("datetime");
            entity.Property(e => e.ActualResourceHrs)
                .HasComment("Number of manufacturing hours used.")
                .HasColumnType("decimal(9, 4)");
            entity.Property(e => e.ActualStartDate)
                .HasComment("Actual start date.")
                .HasColumnType("datetime");
            entity.Property(e => e.LocationId)
                .HasComment("Manufacturing location where the part is processed. Foreign key to Location.LocationID.")
                .HasColumnName("LocationID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.PlannedCost)
                .HasComment("Estimated manufacturing cost.")
                .HasColumnType("money");
            entity.Property(e => e.ScheduledEndDate)
                .HasComment("Planned manufacturing end date.")
                .HasColumnType("datetime");
            entity.Property(e => e.ScheduledStartDate)
                .HasComment("Planned manufacturing start date.")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Location).WithMany(p => p.WorkOrderRoutings)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.WorkOrder).WithMany(p => p.WorkOrderRoutings)
                .HasForeignKey(d => d.WorkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
