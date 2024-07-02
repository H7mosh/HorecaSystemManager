using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sacmy.Server.Models;

namespace sacmy.Server.DatabaseContext;

public partial class SafeenCompanyDbContext : DbContext
{
    public SafeenCompanyDbContext()
    {
    }

    public SafeenCompanyDbContext(DbContextOptions<SafeenCompanyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Balance> Balances { get; set; }

    public virtual DbSet<BarcodStore> BarcodStores { get; set; }

    public virtual DbSet<BnBuyFatoraLastRecord> BnBuyFatoraLastRecords { get; set; }

    public virtual DbSet<BnLastRecord> BnLastRecords { get; set; }

    public virtual DbSet<Bonna> Bonnas { get; set; }

    public virtual DbSet<BonnaCollection> BonnaCollections { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<BuyFatora> BuyFatoras { get; set; }

    public virtual DbSet<BuyFatoraItem> BuyFatoraItems { get; set; }

    public virtual DbSet<Catalog> Catalogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CommentTrackState> CommentTrackStates { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyMassarif> CompanyMassarifs { get; set; }

    public virtual DbSet<CompanyRemainingOld> CompanyRemainingOlds { get; set; }

    public virtual DbSet<CostumerLocation> CostumerLocations { get; set; }

    public virtual DbSet<CostumerType> CostumerTypes { get; set; }

    public virtual DbSet<CountAllCostomerBuyFatora> CountAllCostomerBuyFatoras { get; set; }

    public virtual DbSet<CountAllCostomerPayFrom> CountAllCostomerPayFroms { get; set; }

    public virtual DbSet<CountAllCostomerPayTo> CountAllCostomerPayTos { get; set; }

    public virtual DbSet<CountAllCostomerReturnFatora> CountAllCostomerReturnFatoras { get; set; }

    public virtual DbSet<CountAllCostomerSafi> CountAllCostomerSafis { get; set; }

    public virtual DbSet<CountAllCostomerSafiAll> CountAllCostomerSafiAlls { get; set; }

    public virtual DbSet<CountAllPersonSafi> CountAllPersonSafis { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerBillPoint> CustomerBillPoints { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmpolyeeRole> EmpolyeeRoles { get; set; }

    public virtual DbSet<EventCompany> EventCompanies { get; set; }

    public virtual DbSet<EventCostumer> EventCostumers { get; set; }

    public virtual DbSet<EventDeleted> EventDeleteds { get; set; }

    public virtual DbSet<EventEdited> EventEditeds { get; set; }

    public virtual DbSet<EventIxraci> EventIxracis { get; set; }

    public virtual DbSet<EventMandob> EventMandobs { get; set; }

    public virtual DbSet<EventOffice> EventOffices { get; set; }

    public virtual DbSet<EventOfficeIqd> EventOfficeIqds { get; set; }

    public virtual DbSet<EventPerson> EventPeople { get; set; }

    public virtual DbSet<ExchangeFatora> ExchangeFatoras { get; set; }

    public virtual DbSet<ExchangeFatoraItem> ExchangeFatoraItems { get; set; }

    public virtual DbSet<Factory> Factories { get; set; }

    public virtual DbSet<FeatureItem> FeatureItems { get; set; }

    public virtual DbSet<HorecaImage> HorecaImages { get; set; }

    public virtual DbSet<HorecaInformation> HorecaInformations { get; set; }

    public virtual DbSet<HorecaMenuImage> HorecaMenuImages { get; set; }

    public virtual DbSet<HorecaStatictsInformation> HorecaStatictsInformations { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Item1> Items1 { get; set; }

    public virtual DbSet<ItemCategory> ItemCategories { get; set; }

    public virtual DbSet<ItemImage> ItemImages { get; set; }

    public virtual DbSet<ItemSpecification> ItemSpecifications { get; set; }

    public virtual DbSet<ItemsItemSpecification> ItemsItemSpecifications { get; set; }

    public virtual DbSet<IxraciName> IxraciNames { get; set; }

    public virtual DbSet<Leveel> Leveels { get; set; }

    public virtual DbSet<MandobName> MandobNames { get; set; }

    public virtual DbSet<Masraftype> Masraftypes { get; set; }

    public virtual DbSet<MmMaxzanByWajbaFullItem> MmMaxzanByWajbaFullItems { get; set; }

    public virtual DbSet<MmMaxzanFullItemProcWithOutWajba> MmMaxzanFullItemProcWithOutWajbas { get; set; }

    public virtual DbSet<MmMaxzanFullItemProcWithWajba> MmMaxzanFullItemProcWithWajbas { get; set; }

    public virtual DbSet<MmMaxzanN> MmMaxzanNs { get; set; }

    public virtual DbSet<NnItem> NnItems { get; set; }

    public virtual DbSet<NnPurchaseQtty> NnPurchaseQtties { get; set; }

    public virtual DbSet<NnRaseedItem> NnRaseedItems { get; set; }

    public virtual DbSet<NnRaseedItemsBonna> NnRaseedItemsBonnas { get; set; }

    public virtual DbSet<NnRaseedItemsPasabahceNude> NnRaseedItemsPasabahceNudes { get; set; }

    public virtual DbSet<NnReturnQtty> NnReturnQtties { get; set; }

    public virtual DbSet<NnSalesQtty> NnSalesQtties { get; set; }

    public virtual DbSet<OfficeName> OfficeNames { get; set; }

    public virtual DbSet<Offiice> Offiices { get; set; }

    public virtual DbSet<OnlineOrder> OnlineOrders { get; set; }

    public virtual DbSet<OnlineOrderItem> OnlineOrderItems { get; set; }

    public virtual DbSet<OrderInvoice> OrderInvoices { get; set; }

    public virtual DbSet<OrderInvoiceItem> OrderInvoiceItems { get; set; }

    public virtual DbSet<OtherBrand> OtherBrands { get; set; }

    public virtual DbSet<OtherGlassAgency> OtherGlassAgencies { get; set; }

    public virtual DbSet<PasabahceSeries> PasabahceSeries { get; set; }

    public virtual DbSet<PayFromOffice> PayFromOffices { get; set; }

    public virtual DbSet<PayFromQasa> PayFromQasas { get; set; }

    public virtual DbSet<PayToCustomer> PayToCustomers { get; set; }

    public virtual DbSet<PayToIxraci> PayToIxracis { get; set; }

    public virtual DbSet<PayToMandob> PayToMandobs { get; set; }

    public virtual DbSet<PayToOffice> PayToOffices { get; set; }

    public virtual DbSet<PayToQasa> PayToQasas { get; set; }

    public virtual DbSet<PayfrmCostomer> PayfrmCostomers { get; set; }

    public virtual DbSet<Paytypee> Paytypees { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<PointsReward> PointsRewards { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; }

    public virtual DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }

    public virtual DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }

    public virtual DbSet<PurchaseMasarif> PurchaseMasarifs { get; set; }

    public virtual DbSet<PurchaseSum4search> PurchaseSum4searches { get; set; }

    public virtual DbSet<QasaExChange> QasaExChanges { get; set; }

    public virtual DbSet<QasaExChangeOffice> QasaExChangeOffices { get; set; }

    public virtual DbSet<QiyasUnit> QiyasUnits { get; set; }

    public virtual DbSet<QqArbahForBuyFatora> QqArbahForBuyFatoras { get; set; }

    public virtual DbSet<QqBuyByWajba> QqBuyByWajbas { get; set; }

    public virtual DbSet<QqBuyFatoraItemsForCountCostumer> QqBuyFatoraItemsForCountCostumers { get; set; }

    public virtual DbSet<QqBuyItemsForDatagridview> QqBuyItemsForDatagridviews { get; set; }

    public virtual DbSet<QqBuyItemsForReturnFatoraInserting> QqBuyItemsForReturnFatoraInsertings { get; set; }

    public virtual DbSet<QqCostumerRemainForReport> QqCostumerRemainForReports { get; set; }

    public virtual DbSet<QqCountCompanyPurchaseMasarif> QqCountCompanyPurchaseMasarifs { get; set; }

    public virtual DbSet<QqCountIxraciPurchaseMasarif> QqCountIxraciPurchaseMasarifs { get; set; }

    public virtual DbSet<QqCountPersonPayFromQasa> QqCountPersonPayFromQasas { get; set; }

    public virtual DbSet<QqCountPersonPayToQasa> QqCountPersonPayToQasas { get; set; }

    public virtual DbSet<QqItemsForPurchase> QqItemsForPurchases { get; set; }

    public virtual DbSet<QqMasarifPurchaseeeeeeeeeee> QqMasarifPurchaseeeeeeeeeees { get; set; }

    public virtual DbSet<QqMaxzan> QqMaxzans { get; set; }

    public virtual DbSet<QqMaxzanByWajbaFullItem> QqMaxzanByWajbaFullItems { get; set; }

    public virtual DbSet<QqMaxzanFullItemProc> QqMaxzanFullItemProcs { get; set; }

    public virtual DbSet<QqMaxzanFullItemProcWithWajba> QqMaxzanFullItemProcWithWajbas { get; set; }

    public virtual DbSet<QqMaxzanFullItemProcWithWajbaOldd> QqMaxzanFullItemProcWithWajbaOldds { get; set; }

    public virtual DbSet<QqMaxzanFullItemProcWithoutWajba> QqMaxzanFullItemProcWithoutWajbas { get; set; }

    public virtual DbSet<QqMaxzanFullItemProcWithoutWajbaN> QqMaxzanFullItemProcWithoutWajbaNs { get; set; }

    public virtual DbSet<QqOrderInvoicePrint> QqOrderInvoicePrints { get; set; }

    public virtual DbSet<QqPayFromCostomerReport> QqPayFromCostomerReports { get; set; }

    public virtual DbSet<QqPrintInvoice> QqPrintInvoices { get; set; }

    public virtual DbSet<QqPrintInvoiceMawad> QqPrintInvoiceMawads { get; set; }

    public virtual DbSet<QqPrintSub> QqPrintSubs { get; set; }

    public virtual DbSet<QqPurchaseByWajba> QqPurchaseByWajbas { get; set; }

    public virtual DbSet<QqPurchaseItemsForDatagridview> QqPurchaseItemsForDatagridviews { get; set; }

    public virtual DbSet<QqReturnByWajba> QqReturnByWajbas { get; set; }

    public virtual DbSet<QqReturnByWajbaRr> QqReturnByWajbaRrs { get; set; }

    public virtual DbSet<QqReturnFatoraItemsForCountCostumer> QqReturnFatoraItemsForCountCostumers { get; set; }

    public virtual DbSet<QqReturnItemsForDatagridview> QqReturnItemsForDatagridviews { get; set; }

    public virtual DbSet<QqSalesInvoiceDetail> QqSalesInvoiceDetails { get; set; }

    public virtual DbSet<QqSalesInvoiceDetails2> QqSalesInvoiceDetails2s { get; set; }

    public virtual DbSet<QqSalesInvoiceSumm> QqSalesInvoiceSumms { get; set; }

    public virtual DbSet<QqSalesInvoiceSumm2> QqSalesInvoiceSumm2s { get; set; }

    public virtual DbSet<QqSubbb> QqSubbbs { get; set; }

    public virtual DbSet<QqqCompanyPurchaseInvoicessForPurchaseManager> QqqCompanyPurchaseInvoicessForPurchaseManagers { get; set; }

    public virtual DbSet<QqqCountBuyEventItem> QqqCountBuyEventItems { get; set; }

    public virtual DbSet<QqqCountBuyEventItemsHoreka> QqqCountBuyEventItemsHorekas { get; set; }

    public virtual DbSet<QqqCountBuyEventItemsHorekaNew> QqqCountBuyEventItemsHorekaNews { get; set; }

    public virtual DbSet<QqqCountCompany> QqqCountCompanies { get; set; }

    public virtual DbSet<QqqCountCompanyPurchaseInvoicess> QqqCountCompanyPurchaseInvoicesses { get; set; }

    public virtual DbSet<QqqCountCostumer> QqqCountCostumers { get; set; }

    public virtual DbSet<QqqCountCostumerBuyFatora> QqqCountCostumerBuyFatoras { get; set; }

    public virtual DbSet<QqqCountCostumerNew> QqqCountCostumerNews { get; set; }

    public virtual DbSet<QqqCountCostumerNewTRuning> QqqCountCostumerNewTRunings { get; set; }

    public virtual DbSet<QqqCountCostumerReturnFatora> QqqCountCostumerReturnFatoras { get; set; }

    public virtual DbSet<QqqCountIxraci> QqqCountIxracis { get; set; }

    public virtual DbSet<QqqCountOffice> QqqCountOffices { get; set; }

    public virtual DbSet<QqqCountOfficeFromCurrency> QqqCountOfficeFromCurrencies { get; set; }

    public virtual DbSet<QqqCountOfficeFromCurrencyDollar> QqqCountOfficeFromCurrencyDollars { get; set; }

    public virtual DbSet<QqqCountOfficeFromCurrencyDollarN> QqqCountOfficeFromCurrencyDollarNs { get; set; }

    public virtual DbSet<QqqCountOfficeMultiCurrency> QqqCountOfficeMultiCurrencies { get; set; }

    public virtual DbSet<QqqCountOfficeMultiCurrencyDollar> QqqCountOfficeMultiCurrencyDollars { get; set; }

    public virtual DbSet<QqqCountOfficeMultiCurrencyDollarNn> QqqCountOfficeMultiCurrencyDollarNns { get; set; }

    public virtual DbSet<QqqCountOfficeMultiCurrencyIqd> QqqCountOfficeMultiCurrencyIqds { get; set; }

    public virtual DbSet<QqqCountOfficePayFrmCostumer> QqqCountOfficePayFrmCostumers { get; set; }

    public virtual DbSet<QqqCountOfficePayFrmCostumerDollar> QqqCountOfficePayFrmCostumerDollars { get; set; }

    public virtual DbSet<QqqCountOfficePayFrmCostumerIqd> QqqCountOfficePayFrmCostumerIqds { get; set; }

    public virtual DbSet<QqqCountOfficePayFromOfficeDollar> QqqCountOfficePayFromOfficeDollars { get; set; }

    public virtual DbSet<QqqCountOfficePayToCostumer> QqqCountOfficePayToCostumers { get; set; }

    public virtual DbSet<QqqCountOfficePayToCostumerDollar> QqqCountOfficePayToCostumerDollars { get; set; }

    public virtual DbSet<QqqCountOfficePayToIxraci> QqqCountOfficePayToIxracis { get; set; }

    public virtual DbSet<QqqCountOfficePayToIxraciDollar> QqqCountOfficePayToIxraciDollars { get; set; }

    public virtual DbSet<QqqCountOfficePaytoOffice> QqqCountOfficePaytoOffices { get; set; }

    public virtual DbSet<QqqCountOfficePaytoOfficeDollar> QqqCountOfficePaytoOfficeDollars { get; set; }

    public virtual DbSet<QqqCountOfficeSendMoney> QqqCountOfficeSendMoneys { get; set; }

    public virtual DbSet<QqqCountOfficeSendMoneyDollar> QqqCountOfficeSendMoneyDollars { get; set; }

    public virtual DbSet<QqqCountOfficeTooCurrency> QqqCountOfficeTooCurrencies { get; set; }

    public virtual DbSet<QqqCountOfficeTooCurrencyDollar> QqqCountOfficeTooCurrencyDollars { get; set; }

    public virtual DbSet<QqqCountPerson> QqqCountPeople { get; set; }

    public virtual DbSet<QqqOrderItemManager> QqqOrderItemManagers { get; set; }

    public virtual DbSet<QqqPrintPayToCostomer> QqqPrintPayToCostomers { get; set; }

    public virtual DbSet<QqqPurchaseInvoiceSumm> QqqPurchaseInvoiceSumms { get; set; }

    public virtual DbSet<QqqQtty> QqqQtties { get; set; }

    public virtual DbSet<QqqqCountCompanyAllRemain> QqqqCountCompanyAllRemains { get; set; }

    public virtual DbSet<QqqqCountCompanyPurchase> QqqqCountCompanyPurchases { get; set; }

    public virtual DbSet<QqqqCountCompanyRemainingOld> QqqqCountCompanyRemainingOlds { get; set; }

    public virtual DbSet<QqqqCountCompanyRemainingOldCountt> QqqqCountCompanyRemainingOldCountts { get; set; }

    public virtual DbSet<QqqqCountCompanySendMoney> QqqqCountCompanySendMoneys { get; set; }

    public virtual DbSet<QqqqCountCompanySendMoneyCountt> QqqqCountCompanySendMoneyCountts { get; set; }

    public virtual DbSet<ReName> ReNames { get; set; }

    public virtual DbSet<ReturnFatora> ReturnFatoras { get; set; }

    public virtual DbSet<ReturnFatoraItem> ReturnFatoraItems { get; set; }

    public virtual DbSet<RewardCustomerLinker> RewardCustomerLinkers { get; set; }

    public virtual DbSet<SecondryCategory> SecondryCategories { get; set; }

    public virtual DbSet<SendMoney> SendMoneys { get; set; }

    public virtual DbSet<SeriesOrCollectionOrCategory> SeriesOrCollectionOrCategories { get; set; }

    public virtual DbSet<Specification> Specifications { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<SystemSetting> SystemSettings { get; set; }

    public virtual DbSet<sacmy.Server.Models.Task> Tasks { get; set; }

    public virtual DbSet<TaskNote> TaskNotes { get; set; }

    public virtual DbSet<TblLevel> TblLevels { get; set; }

    public virtual DbSet<TblTutriolVed> TblTutriolVeds { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<TrackComment> TrackComments { get; set; }

    public virtual DbSet<TrackCommentState> TrackCommentStates { get; set; }

    public virtual DbSet<TrackType> TrackTypes { get; set; }

    public virtual DbSet<Typee> Typees { get; set; }

    public virtual DbSet<UnavilableOrderedItem> UnavilableOrderedItems { get; set; }

    public virtual DbSet<Userlogin> Userlogins { get; set; }

    public virtual DbSet<View1> View1s { get; set; }

    public virtual DbSet<WwLastCompanySecode> WwLastCompanySecodes { get; set; }

    public virtual DbSet<WwLastCompanySecodeForOrder> WwLastCompanySecodeForOrders { get; set; }

    public virtual DbSet<WwOrderByCompanySecodeReady> WwOrderByCompanySecodeReadies { get; set; }

    public virtual DbSet<WwwHorekaRetailName> WwwHorekaRetailNames { get; set; }

    public virtual DbSet<ZzOnlineOrderTotal> ZzOnlineOrderTotals { get; set; }

    public virtual DbSet<ZzOrderOnlinePrint> ZzOrderOnlinePrints { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Balance>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Balance");

            entity.Property(e => e.Btotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("BTotal");
            entity.Property(e => e.Btype)
                .HasMaxLength(255)
                .HasColumnName("BType");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("date");
            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Subb)
                .HasMaxLength(100)
                .HasColumnName("subb");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<BarcodStore>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Barcod_Store");

            entity.Property(e => e.BarcodeG).HasColumnName("barcode_G");
        });

        modelBuilder.Entity<BnBuyFatoraLastRecord>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("BN_BuyFatora_LastRecords");

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<BnLastRecord>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("BN_Last_Records");

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<Bonna>(entity =>
        {
            entity.ToTable("Bonna");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Barcode).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Decor).HasMaxLength(50);
            entity.Property(e => e.EdgeChipWarranty).HasMaxLength(50);
            entity.Property(e => e.Glaze).HasMaxLength(50);
            entity.Property(e => e.ProductType).HasMaxLength(50);
        });

        modelBuilder.Entity<BonnaCollection>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BonnaCollection1)
                .HasMaxLength(100)
                .HasColumnName("BonnaCollection");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.HorecaInfo).WithMany(p => p.BonnaCollections)
                .HasForeignKey(d => d.HorecaInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BonnaCollections_HorecaInformations");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NameAr).HasMaxLength(80);
            entity.Property(e => e.NameEn).HasMaxLength(80);
            entity.Property(e => e.NameKr).HasMaxLength(80);
            entity.Property(e => e.NameTr).HasMaxLength(80);
        });

        modelBuilder.Entity<BuyFatora>(entity =>
        {
            entity.ToTable("BuyFatora");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.CostType)
                .HasMaxLength(50)
                .HasColumnName("costType");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnType("datetime")
                .HasColumnName("DATE");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Discount)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("discount");
            entity.Property(e => e.Dolar).HasDefaultValue(0.0);
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Hamalya)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("hamalya");
            entity.Property(e => e.Idd)
                .HasDefaultValue(0)
                .HasColumnName("idd");
            entity.Property(e => e.Ijraaa)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("ijraaa");
            entity.Property(e => e.Ijraaa2)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("ijraaa2");
            entity.Property(e => e.ManCcount)
                .HasDefaultValue(0.0)
                .HasColumnName("man_ccount");
            entity.Property(e => e.Mandob).HasMaxLength(50);
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Nsba).HasDefaultValue(0.0);
            entity.Property(e => e.Payed)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Remaing)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootal)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.TotalPoints).HasColumnName("Total_Points");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<BuyFatoraItem>(entity =>
        {
            entity.HasKey(e => e.BuId);

            entity.ToTable("BuyFatoraItem");

            entity.Property(e => e.BuId).HasColumnName("BU_ID");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Countt).HasDefaultValue(0.0);
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.PurchasePrise)
                .HasColumnType("money")
                .HasColumnName("Purchase_prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttRemaining)
                .HasDefaultValue(0.0)
                .HasColumnName("Qtt_Remaining");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0.0)
                .HasComment("Qtt_Remaining")
                .HasColumnName("quantity");
            entity.Property(e => e.Rub7Karton)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("Rub7_karton");
            entity.Property(e => e.Secode)
                .HasDefaultValue(0)
                .HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
            entity.Property(e => e.Weznn).HasDefaultValue(0f);
        });

        modelBuilder.Entity<Catalog>(entity =>
        {
            entity.ToTable("Catalog");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NameAr).HasMaxLength(80);
            entity.Property(e => e.NameEn).HasMaxLength(80);
            entity.Property(e => e.NameKr).HasMaxLength(80);
            entity.Property(e => e.NameTr).HasMaxLength(80);

            entity.HasOne(d => d.Brand).WithMany(p => p.Catalogs)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Catalog_Brand");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("NameAR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("NameEN");
            entity.Property(e => e.NameKr)
                .HasMaxLength(50)
                .HasColumnName("NameKR");
            entity.Property(e => e.NameTr)
                .HasMaxLength(50)
                .HasColumnName("NameTR");
        });

        modelBuilder.Entity<CommentTrackState>(entity =>
        {
            entity.ToTable("CommentTrackState");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.StateAr).HasMaxLength(50);
            entity.Property(e => e.StateEn).HasMaxLength(50);

            entity.HasOne(d => d.TrackComment).WithMany(p => p.CommentTrackStates)
                .HasForeignKey(d => d.TrackCommentId)
                .HasConstraintName("FK_CommentTrackState_TrackComments");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("company");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Company1)
                .HasMaxLength(50)
                .HasColumnName("company");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<CompanyMassarif>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CompanyMassarif");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Masraftype)
                .HasMaxLength(50)
                .HasColumnName("masraftype");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<CompanyRemainingOld>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Company_Remain_Old");

            entity.ToTable("Company_RemainingOld");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Typee)
                .HasMaxLength(10)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<CostumerLocation>(entity =>
        {
            entity.ToTable("Costumer_Locations");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CostumerLocations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Costumer_Locations_customer");
        });

        modelBuilder.Entity<CostumerType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CostumerType");

            entity.Property(e => e.CostumerType1)
                .HasMaxLength(50)
                .HasColumnName("CostumerType");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Nsba).HasColumnName("nsba");
        });

        modelBuilder.Entity<CountAllCostomerBuyFatora>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Count_ALL_Costomer_BuyFatora");

            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Remain).HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<CountAllCostomerPayFrom>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Count_ALL_Costomer_Pay_FROM");

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Pftotal)
                .HasColumnType("money")
                .HasColumnName("PFTotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<CountAllCostomerPayTo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Count_ALL_Costomer_Pay_To");

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Pttotal)
                .HasColumnType("money")
                .HasColumnName("PTTotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<CountAllCostomerReturnFatora>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Count_ALL_Costomer_ReturnFatora");

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.RetRemain)
                .HasColumnType("money")
                .HasColumnName("Ret_Remain");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<CountAllCostomerSafi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Count_ALL_Costomer_SAFI");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.BuyRemain)
                .HasColumnType("money")
                .HasColumnName("Buy_Remain");
            entity.Property(e => e.CostType)
                .HasMaxLength(50)
                .HasColumnName("costType");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Pftotal)
                .HasColumnType("money")
                .HasColumnName("PFTotal");
            entity.Property(e => e.Pttotal)
                .HasColumnType("money")
                .HasColumnName("PTTotal");
            entity.Property(e => e.RetRemain)
                .HasColumnType("money")
                .HasColumnName("Ret_Remain");
            entity.Property(e => e.Safiremain)
                .HasColumnType("money")
                .HasColumnName("SAFIREMAIN");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<CountAllCostomerSafiAll>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Count_ALL_Costomer_SAFI_ALL");

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Safiremain)
                .HasColumnType("money")
                .HasColumnName("SAFIREMAIN");
        });

        modelBuilder.Entity<CountAllPersonSafi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Count_ALL_Person_SAFI");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Person).HasMaxLength(50);
            entity.Property(e => e.Pfqasa)
                .HasColumnType("money")
                .HasColumnName("PFQasa");
            entity.Property(e => e.Ptoqasa)
                .HasColumnType("money")
                .HasColumnName("PTOQasa");
            entity.Property(e => e.Remain).HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("Currency");

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExchangeRate).HasColumnType("smallmoney");
        });

        modelBuilder.Entity<CurrencyType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("currency_type");

            entity.Property(e => e.CurrencyName)
                .HasMaxLength(50)
                .HasColumnName("currency_Name");
            entity.Property(e => e.CurrencySymbol)
                .HasMaxLength(50)
                .HasColumnName("currency_Symbol");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CostType)
                .HasMaxLength(50)
                .HasColumnName("costType");
            entity.Property(e => e.Costlocat)
                .HasMaxLength(50)
                .HasColumnName("costlocat");
            entity.Property(e => e.Customer1)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.DeviceId).HasColumnName("deviceId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirebaseToken).HasColumnName("firebaseToken");
            entity.Property(e => e.IsPlusOneChecked).HasColumnName("isPlusOneChecked");
            entity.Property(e => e.ManAddress).HasColumnName("Man_address");
            entity.Property(e => e.ManCcount).HasColumnName("man_ccount");
            entity.Property(e => e.ManMob)
                .HasMaxLength(50)
                .HasColumnName("Man_mob");
            entity.Property(e => e.Mandob).HasMaxLength(50);
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.OtherNsba).HasColumnName("otherNsba");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.PlusOne).HasColumnName("plusOne");
            entity.Property(e => e.RefreshToken).HasColumnName("refreshToken");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.UserAcc)
                .HasMaxLength(20)
                .HasColumnName("userAcc");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<CustomerBillPoint>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BillId).HasColumnName("billId");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Points).HasColumnName("points");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerBillPoints)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerBillPoints_customer");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Branch).HasMaxLength(50);
            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.CanclledDate).HasColumnType("datetime");
            entity.Property(e => e.Code)
                .HasMaxLength(70)
                .HasColumnName("code");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(70)
                .HasColumnName("First Name");
            entity.Property(e => e.LastName).HasMaxLength(70);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone number");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Employee_EmpolyeeRole");
        });

        modelBuilder.Entity<EmpolyeeRole>(entity =>
        {
            entity.ToTable("EmpolyeeRole");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        modelBuilder.Entity<EventCompany>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Event_company");

            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .HasColumnName("company");
            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(CONVERT([date],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_event");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.Ttttotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<EventCostumer>(entity =>
        {
            entity.HasKey(e => e.IdEvent);

            entity.ToTable("Event_costumer");

            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.Costumer)
                .HasMaxLength(50)
                .HasColumnName("costumer");
            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(CONVERT([date],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.Ttttotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<EventDeleted>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Event_Deleted");

            entity.Property(e => e.DeletedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.EventTotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("Event_Total");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
            entity.Property(e => e.TypeeventDate).HasColumnName("typeevent_Date");
            entity.Property(e => e.TypeeventId)
                .HasMaxLength(50)
                .HasColumnName("typeevent_ID");
            entity.Property(e => e.TypeeventName)
                .HasMaxLength(50)
                .HasColumnName("typeevent_Name");
            entity.Property(e => e.UserDeleted)
                .HasMaxLength(50)
                .HasColumnName("User_deleted");
        });

        modelBuilder.Entity<EventEdited>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Event_Edited");

            entity.Property(e => e.DeletedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.EventTotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("Event_Total");
            entity.Property(e => e.EventTotalNew)
                .HasColumnType("money")
                .HasColumnName("Event_TotalNew");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
            entity.Property(e => e.TypeeventDate).HasColumnName("typeevent_Date");
            entity.Property(e => e.TypeeventId)
                .HasMaxLength(50)
                .HasColumnName("typeevent_ID");
            entity.Property(e => e.TypeeventName)
                .HasMaxLength(50)
                .HasColumnName("typeevent_Name");
            entity.Property(e => e.UserDeleted)
                .HasMaxLength(50)
                .HasColumnName("User_deleted");
        });

        modelBuilder.Entity<EventIxraci>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Event_ixraci");

            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(CONVERT([date],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_event");
            entity.Property(e => e.IxraciN)
                .HasMaxLength(50)
                .HasColumnName("ixraciN");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.Ttttotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<EventMandob>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Event_Mandob");

            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(CONVERT([date],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_event");
            entity.Property(e => e.Mandob).HasMaxLength(50);
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.Ttttotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<EventOffice>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Event_Office");

            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(CONVERT([date],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_event");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Officee)
                .HasMaxLength(50)
                .HasColumnName("officee");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.Ttttotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<EventOfficeIqd>(entity =>
        {
            entity.HasKey(e => e.IdEvent);

            entity.ToTable("Event_Office_IQD");

            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(CONVERT([date],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Officee)
                .HasMaxLength(50)
                .HasColumnName("officee");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.Ttttotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<EventPerson>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Event_Person");

            entity.Property(e => e.Costumer)
                .HasMaxLength(50)
                .HasColumnName("costumer");
            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(CONVERT([date],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_event");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.Ttttotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<ExchangeFatora>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ExchangeFatora");

            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .HasColumnName("company");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("DATE");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.FromStor)
                .HasMaxLength(50)
                .HasColumnName("fromStor");
            entity.Property(e => e.IdExcha)
                .HasDefaultValue(1)
                .HasColumnName("ID_Excha");
            entity.Property(e => e.IdPurch).HasColumnName("id_Purch");
            entity.Property(e => e.IdSales).HasColumnName("id_sales");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PuEventId)
                .HasMaxLength(100)
                .HasColumnName("Pu_event_id");
            entity.Property(e => e.SalEventId)
                .HasMaxLength(100)
                .HasColumnName("sal_event_id");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.TooStor).HasMaxLength(50);
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<ExchangeFatoraItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ExchangeFatoraItem");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.BuId)
                .ValueGeneratedOnAdd()
                .HasColumnName("BU_ID");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Countt).HasDefaultValue(0.0);
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.PurchasePrise)
                .HasColumnType("money")
                .HasColumnName("Purchase_prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttRemaining)
                .HasDefaultValue(0.0)
                .HasColumnName("Qtt_Remaining");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0.0)
                .HasComment("Qtt_Remaining")
                .HasColumnName("quantity");
            entity.Property(e => e.Rub7Karton)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("Rub7_karton");
            entity.Property(e => e.Secode)
                .HasDefaultValue(0)
                .HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
            entity.Property(e => e.Weznn).HasDefaultValue(0f);
        });

        modelBuilder.Entity<Factory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("factory");

            entity.Property(e => e.Factory1)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<FeatureItem>(entity =>
        {
            entity.ToTable("FeatureItem");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.SkuCod)
                .HasMaxLength(50)
                .HasColumnName("SKU_cod");

            entity.HasOne(d => d.Item).WithMany(p => p.FeatureItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FeatureItem_Items");
        });

        modelBuilder.Entity<HorecaImage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Image).HasColumnName("image");

            entity.HasOne(d => d.HorecaInfo).WithMany(p => p.HorecaImages)
                .HasForeignKey(d => d.HorecaInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HorecaImages_HorecaInformations");
        });

        modelBuilder.Entity<HorecaInformation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Concept).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Governorate).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.LocationDescription)
                .HasMaxLength(80)
                .HasColumnName("Location_description");
            entity.Property(e => e.OwnerPhone).HasMaxLength(50);
            entity.Property(e => e.PurchasingManagerPhone).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.HorecaInformations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_HorecaInformations_Employee");
        });

        modelBuilder.Entity<HorecaMenuImage>(entity =>
        {
            entity.ToTable("HorecaMenuImage");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Createddate).HasColumnType("datetime");
            entity.Property(e => e.Image).HasColumnName("image");

            entity.HasOne(d => d.HorecaInfo).WithMany(p => p.HorecaMenuImages)
                .HasForeignKey(d => d.HorecaInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HorecaMenuImage_HorecaInformations");
        });

        modelBuilder.Entity<HorecaStatictsInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HorecaStatictsCompany");

            entity.ToTable("HorecaStatictsInformation");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsHeNudeBuyer).HasColumnName("isHeNudeBuyer");

            entity.HasOne(d => d.HorecaInfo).WithMany(p => p.HorecaStatictsInformations)
                .HasForeignKey(d => d.HorecaInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HorecaStatictsInformation_HorecaInformations");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("item");

            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Count).HasDefaultValue(0.0);
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Imagling)
                .HasMaxLength(250)
                .HasColumnName("imagling");
            entity.Property(e => e.Inn)
                .HasMaxLength(50)
                .HasColumnName("inn");
            entity.Property(e => e.IsplusOneChecked).HasColumnName("isplusOneChecked");
            entity.Property(e => e.Item1)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Outt)
                .HasMaxLength(50)
                .HasColumnName("outt");
            entity.Property(e => e.PlusOne).HasColumnName("plusOne");
            entity.Property(e => e.Prise)
                .HasDefaultValue(0.0)
                .HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Secode)
                .HasDefaultValue(1)
                .HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<Item1>(entity =>
        {
            entity.ToTable("Items");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Base).HasMaxLength(50);
            entity.Property(e => e.Catalog).HasMaxLength(50);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Ean).HasMaxLength(50);
            entity.Property(e => e.Function).HasMaxLength(50);
            entity.Property(e => e.GlassType).HasMaxLength(50);
            entity.Property(e => e.InnerType).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OuterType).HasMaxLength(30);
            entity.Property(e => e.PattrenNo).HasMaxLength(50);
            entity.Property(e => e.ProductionTechnique).HasMaxLength(50);
            entity.Property(e => e.SeCode).HasMaxLength(50);
            entity.Property(e => e.Series).HasMaxLength(80);
            entity.Property(e => e.Sku).HasMaxLength(50);
            entity.Property(e => e.Upc).HasMaxLength(50);

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Item1s)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Items_Category");

            entity.HasOne(d => d.SecondryCategory).WithMany(p => p.Item1s)
                .HasForeignKey(d => d.SecondryCategoryId)
                .HasConstraintName("FK_Items_SecondryCategory");
        });

        modelBuilder.Entity<ItemCategory>(entity =>
        {
            entity.ToTable("ItemCategory");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ItemImage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Link).HasColumnName("link");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemImages)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK_ItemImages_Items");
        });

        modelBuilder.Entity<ItemSpecification>(entity =>
        {
            entity.ToTable("ItemSpecification");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DescriptionAr)
                .HasMaxLength(50)
                .HasColumnName("DescriptionAR");
            entity.Property(e => e.DescriptionEn)
                .HasMaxLength(100)
                .HasColumnName("DescriptionEN");
            entity.Property(e => e.DescriptionKr)
                .HasMaxLength(100)
                .HasColumnName("DescriptionKR");
            entity.Property(e => e.DescriptionTr)
                .HasMaxLength(150)
                .HasColumnName("DescriptionTR");
            entity.Property(e => e.ImageName).HasMaxLength(100);
            entity.Property(e => e.TitleAr).HasMaxLength(50);
            entity.Property(e => e.TitleEn)
                .HasMaxLength(50)
                .HasColumnName("TitleEN");
            entity.Property(e => e.TitleKr)
                .HasMaxLength(50)
                .HasColumnName("TitleKR");
            entity.Property(e => e.TitleTr)
                .HasMaxLength(50)
                .HasColumnName("TitleTR");
        });

        modelBuilder.Entity<ItemsItemSpecification>(entity =>
        {
            entity.ToTable("Items_ItemSpecification");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Item).WithMany(p => p.ItemsItemSpecifications)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Items_ItemSpecification_Relation");

            entity.HasOne(d => d.Specifications).WithMany(p => p.ItemsItemSpecifications)
                .HasForeignKey(d => d.SpecificationsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Items_ItemSpecification_ItemSpecification_Relation");
        });

        modelBuilder.Entity<IxraciName>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ixraci_Name");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IxraciN).HasMaxLength(50);
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<Leveel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("leveel");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Leveel1)
                .HasMaxLength(50)
                .HasColumnName("leveel");
        });

        modelBuilder.Entity<MandobName>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MandobName");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ManAddress)
                .HasMaxLength(50)
                .HasColumnName("Man_address");
            entity.Property(e => e.ManCcount).HasColumnName("man_ccount");
            entity.Property(e => e.ManMob)
                .HasMaxLength(50)
                .HasColumnName("Man_mob");
            entity.Property(e => e.Mandob).HasMaxLength(50);
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .HasColumnName("pass");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Uusername)
                .HasMaxLength(50)
                .HasColumnName("uusername");
        });

        modelBuilder.Entity<Masraftype>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("masraftype");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Masraftype1)
                .HasMaxLength(50)
                .HasColumnName("masraftype");
        });

        modelBuilder.Entity<MmMaxzanByWajbaFullItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MM_Maxzan_By_Wajba_Full_Items");

            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttyPurch).HasColumnName("Qtty_Purch");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySales).HasColumnName("Qtty_Sales");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smtotal).HasColumnName("SMtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.TotalRemain).HasColumnName("Total_Remain");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<MmMaxzanFullItemProcWithOutWajba>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MM_Maxzan_Full_Item_Proc_with_out_Wajba");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttyPurch).HasColumnName("Qtty_Purch");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySales).HasColumnName("Qtty_Sales");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.TotalRemain).HasColumnName("Total_Remain");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<MmMaxzanFullItemProcWithWajba>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MM_Maxzan_Full_Item_Proc_with_Wajba");

            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttyPurch).HasColumnName("Qtty_Purch");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySales).HasColumnName("Qtty_Sales");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smtotal).HasColumnName("SMtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.TotalRemain).HasColumnName("Total_Remain");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<MmMaxzanN>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MM_Maxzan_N");

            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.QttyPurch).HasColumnName("Qtty_Purch");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySales).HasColumnName("Qtty_Sales");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smtotal).HasColumnName("SMtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<NnItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NN_Items");

            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Imagling)
                .HasMaxLength(250)
                .HasColumnName("imagling");
            entity.Property(e => e.Inn)
                .HasMaxLength(50)
                .HasColumnName("inn");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Outt)
                .HasMaxLength(50)
                .HasColumnName("outt");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<NnPurchaseQtty>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NN_Purchase_Qtty");

            entity.Property(e => e.PurchQtty).HasColumnName("Purch_Qtty");
            entity.Property(e => e.Secode).HasColumnName("secode");
        });

        modelBuilder.Entity<NnRaseedItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NN_Raseed_Items");

            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.PurchQtty).HasColumnName("Purch_Qtty");
            entity.Property(e => e.RemainQtty).HasColumnName("Remain_Qtty");
            entity.Property(e => e.ReturnQtty).HasColumnName("Return_Qtty");
            entity.Property(e => e.SalesQtty).HasColumnName("Sales_Qtty");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<NnRaseedItemsBonna>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NN_Raseed_Items_Bonna");

            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Inn)
                .HasMaxLength(50)
                .HasColumnName("inn");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Outt)
                .HasMaxLength(50)
                .HasColumnName("outt");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.PurchQtty).HasColumnName("Purch_Qtty");
            entity.Property(e => e.RemainQtty).HasColumnName("Remain_Qtty");
            entity.Property(e => e.ReturnQtty).HasColumnName("Return_Qtty");
            entity.Property(e => e.SalesQtty).HasColumnName("Sales_Qtty");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<NnRaseedItemsPasabahceNude>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NN_Raseed_Items_Pasabahce_NUDE");

            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Inn)
                .HasMaxLength(50)
                .HasColumnName("inn");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Outt)
                .HasMaxLength(50)
                .HasColumnName("outt");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.PurchQtty).HasColumnName("Purch_Qtty");
            entity.Property(e => e.RemainQtty).HasColumnName("Remain_Qtty");
            entity.Property(e => e.ReturnQtty).HasColumnName("Return_Qtty");
            entity.Property(e => e.SalesQtty).HasColumnName("Sales_Qtty");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<NnReturnQtty>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NN_Return_Qtty");

            entity.Property(e => e.ReturnQtty).HasColumnName("Return_Qtty");
            entity.Property(e => e.Secode).HasColumnName("secode");
        });

        modelBuilder.Entity<NnSalesQtty>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NN_Sales_Qtty");

            entity.Property(e => e.SalesQtty).HasColumnName("Sales_Qtty");
            entity.Property(e => e.Secode).HasColumnName("secode");
        });

        modelBuilder.Entity<OfficeName>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Office_Name");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CurrencySymbol).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.OfficeN).HasMaxLength(50);
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<Offiice>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("offiice");

            entity.Property(e => e.Addresss)
                .HasMaxLength(50)
                .HasColumnName("addresss");
            entity.Property(e => e.EMail)
                .HasMaxLength(50)
                .HasColumnName("e_mail");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Mob).HasMaxLength(50);
            entity.Property(e => e.Offiice1)
                .HasMaxLength(50)
                .HasColumnName("offiice");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<OnlineOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("Online_Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("address");
            entity.Property(e => e.Checked).HasColumnName("checked");
            entity.Property(e => e.CostumerName)
                .HasMaxLength(50)
                .HasColumnName("Costumer_Name");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(350);
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("now");
            entity.Property(e => e.Sub).HasMaxLength(50);
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<OnlineOrderItem>(entity =>
        {
            entity.HasKey(e => e.OrR);

            entity.ToTable("Online_Order_Items");

            entity.Property(e => e.OrR).HasColumnName("Or_R");
            entity.Property(e => e.Barcod).HasMaxLength(50);
            entity.Property(e => e.CancelledDate).HasColumnType("datetime");
            entity.Property(e => e.Item).HasMaxLength(250);
            entity.Property(e => e.MoldNo).HasMaxLength(50);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Secod)
                .HasMaxLength(50)
                .HasColumnName("secod");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("SKU");
            entity.Property(e => e.Sub).HasMaxLength(50);
            entity.Property(e => e.Total).HasColumnType("money");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.OnlineOrderItems)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("CategoryOrderRelation");

            entity.HasOne(d => d.ItemNavigation).WithMany(p => p.OnlineOrderItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OnlineOrderItemsRelation");

            entity.HasOne(d => d.SecondryCategory).WithMany(p => p.OnlineOrderItems)
                .HasForeignKey(d => d.SecondryCategoryId)
                .HasConstraintName("FK_Online_Order_Items_SecondryCategory");
        });

        modelBuilder.Entity<OrderInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Order_Invoice");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("DATE");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("ID");
            entity.Property(e => e.Idd)
                .HasDefaultValue(0)
                .HasColumnName("idd");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootal)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<OrderInvoiceItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Order_Invoice_ITEM");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Countt).HasDefaultValue(0.0);
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.OrId)
                .ValueGeneratedOnAdd()
                .HasColumnName("OR_ID");
            entity.Property(e => e.Prise)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0.0)
                .HasComment("Qtt_Remaining")
                .HasColumnName("quantity");
            entity.Property(e => e.Secode)
                .HasDefaultValue(0)
                .HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
        });

        modelBuilder.Entity<OtherBrand>(entity =>
        {
            entity.ToTable("OtherBrand");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BrandName).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.HorecaInfo).WithMany(p => p.OtherBrands)
                .HasForeignKey(d => d.HorecaInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OtherBrand_HorecaInformations");
        });

        modelBuilder.Entity<OtherGlassAgency>(entity =>
        {
            entity.ToTable("OtherGlassAgency");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.GlassAgencyName).HasMaxLength(50);

            entity.HasOne(d => d.HorecaInfo).WithMany(p => p.OtherGlassAgencies)
                .HasForeignKey(d => d.HorecaInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OtherGlassAgency_HorecaInformations");
        });

        modelBuilder.Entity<PasabahceSeries>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Series).HasMaxLength(100);

            entity.HasOne(d => d.HorecaInfo).WithMany(p => p.PasabahceSeries)
                .HasForeignKey(d => d.HorecaInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PasabahceSeries_HorecaInformations");
        });

        modelBuilder.Entity<PayFromOffice>(entity =>
        {
            entity.ToTable("Pay_From_Office");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("date");
            entity.Property(e => e.Dolar)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IqdTtal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("IQD_Ttal");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Offiice)
                .HasMaxLength(50)
                .HasColumnName("offiice");
            entity.Property(e => e.Ptotal)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<PayFromQasa>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Pay_from_qasa");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.ReName)
                .HasMaxLength(255)
                .HasColumnName("Re_Name");
            entity.Property(e => e.Subb)
                .HasMaxLength(100)
                .HasColumnName("subb");
            entity.Property(e => e.Toetal)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<PayToCustomer>(entity =>
        {
            entity.ToTable("PayToCustomer");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.ToOffice)
                .HasMaxLength(50)
                .HasColumnName("To_Office");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Typee)
                .HasMaxLength(10)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<PayToIxraci>(entity =>
        {
            entity.ToTable("PayTo_ixraci");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("date");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.FrmOffice)
                .HasMaxLength(50)
                .HasColumnName("Frm_Office");
            entity.Property(e => e.IxraciN)
                .HasMaxLength(50)
                .HasColumnName("ixraciN");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Typee)
                .HasMaxLength(10)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<PayToMandob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PayToMandob_1");

            entity.ToTable("PayToMandob");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Mandob).HasMaxLength(50);
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PayType).HasMaxLength(50);
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.ToOffice)
                .HasMaxLength(50)
                .HasColumnName("To_Office");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<PayToOffice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PayToOffice3_1");

            entity.ToTable("PayToOffice");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("date");
            entity.Property(e => e.Dolar)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IqdTtal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("IQD_Ttal");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Offiice)
                .HasMaxLength(50)
                .HasColumnName("offiice");
            entity.Property(e => e.Ptotal)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<PayToQasa>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Pay_To_qasa");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("date");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.ReName)
                .HasMaxLength(255)
                .HasColumnName("Re_Name");
            entity.Property(e => e.ReTotal)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(100)
                .HasColumnName("subb");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<PayfrmCostomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PayfrmCostomer_1");

            entity.ToTable("PayfrmCostomer");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Dolar)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Iqd)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("IQD");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PayType).HasMaxLength(50);
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.ToOffice)
                .HasMaxLength(50)
                .HasColumnName("To_Office");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<Paytypee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("paytypee");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Ptype)
                .HasMaxLength(50)
                .HasColumnName("ptype");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PersonS");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Person1)
                .HasMaxLength(50)
                .HasColumnName("Person");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<PointsReward>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BaseUnit).HasMaxLength(70);
            entity.Property(e => e.BoxType).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.HeightUnit).HasMaxLength(70);
            entity.Property(e => e.LengthUnit).HasMaxLength(70);
            entity.Property(e => e.PieceType).HasMaxLength(50);
            entity.Property(e => e.Sku).HasMaxLength(20);
            entity.Property(e => e.TopUnit).HasMaxLength(70);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.WeightUnit).HasMaxLength(70);

            entity.HasOne(d => d.SeriesOrCollection).WithMany(p => p.Products)
                .HasForeignKey(d => d.SeriesOrCollectionId)
                .HasConstraintName("FK_Products_SeriesOrCollection");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Link)
                .HasMaxLength(200)
                .HasColumnName("link");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductImages_Products");
        });

        modelBuilder.Entity<ProductSpecification>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductSpecifications_Products");

            entity.HasOne(d => d.Specification).WithMany()
                .HasForeignKey(d => d.SpecificationId)
                .HasConstraintName("FK_ProductSpecifications_Specifications");
        });

        modelBuilder.Entity<PurchaseInvoice>(entity =>
        {
            entity.ToTable("Purchase_Invoice");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("DATE");
            entity.Property(e => e.Dolar)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Idd)
                .HasDefaultValue(0)
                .HasColumnName("idd");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootal)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<PurchaseInvoiceItem>(entity =>
        {
            entity.HasKey(e => e.PuId);

            entity.ToTable("Purchase_Invoice_ITEMS");

            entity.Property(e => e.PuId).HasColumnName("PU_ID");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Countt).HasDefaultValue(0.0);
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.FirstQtty)
                .HasDefaultValue(0.0)
                .HasComment("Qtt_Remaining");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Masraf).HasColumnType("money");
            entity.Property(e => e.Naqis)
                .HasDefaultValue(0.0)
                .HasComment("Qtt_Remaining");
            entity.Property(e => e.Prise)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0.0)
                .HasComment("Qtt_Remaining")
                .HasColumnName("quantity");
            entity.Property(e => e.Secode)
                .HasDefaultValue(0)
                .HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.TotlPrise).HasColumnType("money");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
            entity.Property(e => e.Weznn).HasDefaultValue(0f);
            entity.Property(e => e.Ziyada)
                .HasDefaultValue(0.0)
                .HasComment("Qtt_Remaining");
        });

        modelBuilder.Entity<PurchaseMasarif>(entity =>
        {
            entity.ToTable("Purchase_masarif");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("ID");
            entity.Property(e => e.Amola)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("amola");
            entity.Property(e => e.Datee)
                .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))")
                .HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Gomrk)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("gomrk");
            entity.Property(e => e.Hamalya)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("hamalya");
            entity.Property(e => e.Ijra)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("ijra");
            entity.Property(e => e.IxraciN)
                .HasMaxLength(30)
                .HasColumnName("ixraciN");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Oxra)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("oxra");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootaal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("tootaal");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<PurchaseSum4search>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Purchase_Sum_4Search");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Stotal)
                .HasColumnType("money")
                .HasColumnName("STOTAL");
        });

        modelBuilder.Entity<QasaExChange>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Qasa_ExChange");

            entity.Property(e => e.CurrencyPrice).HasColumnType("money");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Qasa)
                .HasMaxLength(50)
                .HasDefaultValue("Qasa_IQD");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.SymbolL).HasMaxLength(50);
            entity.Property(e => e.Tootall).HasColumnType("money");
            entity.Property(e => e.TotaLlafter)
                .HasColumnType("money")
                .HasColumnName("totaLLAfter");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QasaExChangeOffice>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Qasa_ExChange_Office");

            entity.Property(e => e.CurrencyPrice).HasColumnType("money");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.EventIdOther)
                .HasMaxLength(100)
                .HasColumnName("event_id_Other");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Office).HasMaxLength(25);
            entity.Property(e => e.OfficeOther)
                .HasMaxLength(25)
                .HasColumnName("Office_Other");
            entity.Property(e => e.Qasa)
                .HasMaxLength(50)
                .HasDefaultValue("Qasa_IQD");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.SymbolL).HasMaxLength(50);
            entity.Property(e => e.Tootall).HasColumnType("money");
            entity.Property(e => e.TotaLlafter)
                .HasColumnType("money")
                .HasColumnName("totaLLAfter");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QiyasUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("qiyas_unit");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.QiyasUnit1)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
        });

        modelBuilder.Entity<QqArbahForBuyFatora>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Arbah_For_Buy_Fatora");

            entity.Property(e => e.ArbahForEchQtty).HasColumnType("money");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idd).HasColumnName("idd");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.PurchasePrise)
                .HasColumnType("money")
                .HasColumnName("Purchase_prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqBuyByWajba>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Buy_By_Wajba");

            entity.Property(e => e.QttySales).HasColumnName("Qtty_Sales");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqBuyFatoraItemsForCountCostumer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Buy_Fatora_Items_For_CountCostumer");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.BuId).HasColumnName("BU_ID");
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Hamalya)
                .HasColumnType("money")
                .HasColumnName("hamalya");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idd).HasColumnName("idd");
            entity.Property(e => e.Ijraaa)
                .HasColumnType("money")
                .HasColumnName("ijraaa");
            entity.Property(e => e.Itemm)
                .HasMaxLength(50)
                .HasColumnName("itemm");
            entity.Property(e => e.ManCcount).HasColumnName("man_ccount");
            entity.Property(e => e.Mandob).HasMaxLength(50);
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Payed).HasColumnType("money");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Remaing).HasColumnType("money");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootal).HasColumnType("money");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqBuyItemsForDatagridview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Buy_Items_For_Datagridview");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.BuId).HasColumnName("BU_ID");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(50)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.PurchasePrise)
                .HasColumnType("money")
                .HasColumnName("Purchase_prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttRemaining).HasColumnName("Qtt_Remaining");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rub7Karton)
                .HasColumnType("money")
                .HasColumnName("Rub7_karton");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqBuyItemsForReturnFatoraInserting>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Buy_Items_For_Return_Fatora_Inserting");

            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqCostumerRemainForReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Costumer_Remain_For_Reports");

            entity.Property(e => e.Costumer)
                .HasMaxLength(50)
                .HasColumnName("costumer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Runningtotal)
                .HasColumnType("money")
                .HasColumnName("runningtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<QqCountCompanyPurchaseMasarif>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_count_Company_Purchase_Masarif");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Amola)
                .HasColumnType("money")
                .HasColumnName("amola");
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.Gomrk)
                .HasColumnType("money")
                .HasColumnName("gomrk");
            entity.Property(e => e.Hamalya)
                .HasColumnType("money")
                .HasColumnName("hamalya");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idd).HasColumnName("idd");
            entity.Property(e => e.Ijra)
                .HasColumnType("money")
                .HasColumnName("ijra");
            entity.Property(e => e.IxraciN)
                .HasMaxLength(30)
                .HasColumnName("ixraciN");
            entity.Property(e => e.MNote)
                .HasColumnType("ntext")
                .HasColumnName("M_Note");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Oxra)
                .HasColumnType("money")
                .HasColumnName("oxra");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootaal)
                .HasColumnType("money")
                .HasColumnName("tootaal");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqCountIxraciPurchaseMasarif>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_count_Ixraci_Purchase_Masarif");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Amola)
                .HasColumnType("money")
                .HasColumnName("amola");
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.Gomrk)
                .HasColumnType("money")
                .HasColumnName("gomrk");
            entity.Property(e => e.Hamalya)
                .HasColumnType("money")
                .HasColumnName("hamalya");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idd).HasColumnName("idd");
            entity.Property(e => e.Ijra)
                .HasColumnType("money")
                .HasColumnName("ijra");
            entity.Property(e => e.IxraciN)
                .HasMaxLength(30)
                .HasColumnName("ixraciN");
            entity.Property(e => e.MNote)
                .HasColumnType("ntext")
                .HasColumnName("M_Note");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Oxra)
                .HasColumnType("money")
                .HasColumnName("oxra");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootaal)
                .HasColumnType("money")
                .HasColumnName("tootaal");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqCountPersonPayFromQasa>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Count_Person_Pay_From_Qasa");

            entity.Property(e => e.ReName)
                .HasMaxLength(255)
                .HasColumnName("Re_Name");
            entity.Property(e => e.Subb)
                .HasMaxLength(100)
                .HasColumnName("subb");
            entity.Property(e => e.Toetal).HasColumnType("money");
        });

        modelBuilder.Entity<QqCountPersonPayToQasa>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Count_Person_Pay_To_Qasa");

            entity.Property(e => e.ReName)
                .HasMaxLength(255)
                .HasColumnName("Re_Name");
            entity.Property(e => e.ReTotal).HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(100)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqItemsForPurchase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Items_For_Purchase");

            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(50)
                .HasColumnName("item");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<QqMasarifPurchaseeeeeeeeeee>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Masarif_Purchaseeeeeeeeeee");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Amola)
                .HasColumnType("money")
                .HasColumnName("amola");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Expr1).HasMaxLength(25);
            entity.Property(e => e.Gomrk)
                .HasColumnType("money")
                .HasColumnName("gomrk");
            entity.Property(e => e.Hamalya)
                .HasColumnType("money")
                .HasColumnName("hamalya");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Ijra)
                .HasColumnType("money")
                .HasColumnName("ijra");
            entity.Property(e => e.IxraciN)
                .HasMaxLength(30)
                .HasColumnName("ixraciN");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Oxra)
                .HasColumnType("money")
                .HasColumnName("oxra");
            entity.Property(e => e.Ssstotal)
                .HasColumnType("money")
                .HasColumnName("SSSTotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootaal)
                .HasColumnType("money")
                .HasColumnName("tootaal");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqMaxzan>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Maxzan");

            entity.Property(e => e.QttyPurch).HasColumnName("Qtty_Purch");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySales).HasColumnName("Qtty_Sales");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smtotal).HasColumnName("SMtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqMaxzanByWajbaFullItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Maxzan_By_Wajba_Full_Item");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttyPurch).HasColumnName("Qtty_Purch");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySales).HasColumnName("Qtty_Sales");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smtotal).HasColumnName("SMtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.TotalRemain).HasColumnName("Total_Remain");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqMaxzanFullItemProc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Maxzan_Full_Item_Proc");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(50)
                .HasColumnName("item");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttyPurch).HasColumnName("Qtty_Purch");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySales).HasColumnName("Qtty_Sales");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smtotal).HasColumnName("SMtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqMaxzanFullItemProcWithWajba>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Maxzan_Full_Item_Proc_with_Wajba");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttyPurch).HasColumnName("Qtty_Purch");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySales).HasColumnName("Qtty_Sales");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smtotal).HasColumnName("SMtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.TotalRemain).HasColumnName("Total_Remain");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqMaxzanFullItemProcWithWajbaOldd>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Maxzan_Full_Item_Proc_with_Wajba_oldd");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttyPurch).HasColumnName("Qtty_Purch");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySales).HasColumnName("Qtty_Sales");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smtotal).HasColumnName("SMtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.TotalRemain).HasColumnName("Total_Remain");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqMaxzanFullItemProcWithoutWajba>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Maxzan_Full_Item_Proc_withoutWajba");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(50)
                .HasColumnName("item");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttyPurchh).HasColumnName("Qtty_Purchh");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySaless).HasColumnName("Qtty_Saless");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Ssmtotal).HasColumnName("SSMtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<QqMaxzanFullItemProcWithoutWajbaN>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Maxzan_Full_Item_Proc_withoutWajba_N");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Prise).HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttyPurchas).HasColumnName("Qtty_Purchas");
            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.QttySaless).HasColumnName("Qtty_Saless");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smtotall).HasColumnName("SMtotall");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.TotalRemain).HasColumnName("Total_Remain");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<QqOrderInvoicePrint>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Order_Invoice_Print");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idd).HasColumnName("idd");
            entity.Property(e => e.Inn)
                .HasMaxLength(50)
                .HasColumnName("inn");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Outt)
                .HasMaxLength(50)
                .HasColumnName("outt");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootal).HasColumnType("money");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqPayFromCostomerReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Pay_From_Costomer_Report");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Dolar).HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Iqd)
                .HasColumnType("money")
                .HasColumnName("IQD");
            entity.Property(e => e.LastRemain)
                .HasColumnType("money")
                .HasColumnName("LAST_REMAIN");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PayType).HasMaxLength(50);
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Safiremain)
                .HasColumnType("money")
                .HasColumnName("SAFIREMAIN");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.ToOffice)
                .HasMaxLength(50)
                .HasColumnName("To_Office");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqPrintInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Print_Invoice");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Descriptioneng)
                .HasMaxLength(255)
                .HasColumnName("descriptioneng");
            entity.Property(e => e.DiscontIqd).HasColumnName("DiscontIQD");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Expr1).HasMaxLength(200);
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.HamaliyaIqd).HasColumnName("HamaliyaIQD");
            entity.Property(e => e.Hamalya)
                .HasColumnType("money")
                .HasColumnName("hamalya");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idd).HasColumnName("idd");
            entity.Property(e => e.Ijraaa)
                .HasColumnType("money")
                .HasColumnName("ijraaa");
            entity.Property(e => e.Ijraaa2)
                .HasColumnType("money")
                .HasColumnName("ijraaa2");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.LastRemain)
                .HasColumnType("money")
                .HasColumnName("LAST_REMAIN");
            entity.Property(e => e.ManCcount).HasColumnName("man_ccount");
            entity.Property(e => e.Mandob).HasMaxLength(50);
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Mob1)
                .HasMaxLength(50)
                .HasColumnName("mob1");
            entity.Property(e => e.Mob2)
                .HasMaxLength(50)
                .HasColumnName("mob2");
            entity.Property(e => e.Mob3)
                .HasMaxLength(50)
                .HasColumnName("mob3");
            entity.Property(e => e.Mob4)
                .HasMaxLength(50)
                .HasColumnName("mob4");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.NameEng)
                .HasMaxLength(50)
                .HasColumnName("nameEng");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PayIqd).HasColumnName("PayIQD");
            entity.Property(e => e.Payed).HasColumnType("money");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.PriseIqd).HasColumnName("PriseIQD");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.RemainIqd).HasColumnName("RemainIQD");
            entity.Property(e => e.Remaing).HasColumnType("money");
            entity.Property(e => e.Safiremain)
                .HasColumnType("money")
                .HasColumnName("SAFIREMAIN");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smmkaab).HasColumnName("SMMkaab");
            entity.Property(e => e.Smmkaabb).HasColumnName("SMMkaabb");
            entity.Property(e => e.Smqtty).HasColumnName("SMQtty");
            entity.Property(e => e.Smtotal)
                .HasColumnType("money")
                .HasColumnName("SMtotal");
            entity.Property(e => e.Smweznn).HasColumnName("SMWeznn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.SubbB)
                .HasMaxLength(25)
                .HasColumnName("subbB");
            entity.Property(e => e.SumTotalIqd).HasColumnName("SumTotalIQD");
            entity.Property(e => e.Summwezn).HasColumnName("SUMMWezn");
            entity.Property(e => e.SummweznTotal).HasColumnName("SUMMWeznTotal");
            entity.Property(e => e.Sumqtty).HasColumnName("SUMQtty");
            entity.Property(e => e.Tootal).HasColumnType("money");
            entity.Property(e => e.TotalIqd).HasColumnName("TotalIQD");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Web)
                .HasMaxLength(50)
                .HasColumnName("WEB");
        });

        modelBuilder.Entity<QqPrintInvoiceMawad>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Print_Invoice-Mawad");

            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smqtty).HasColumnName("SMQtty");
            entity.Property(e => e.Smtotal)
                .HasColumnType("money")
                .HasColumnName("SMtotal");
            entity.Property(e => e.Smweznn).HasColumnName("SMWeznn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqPrintSub>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Print_SUB");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Descriptioneng)
                .HasMaxLength(255)
                .HasColumnName("descriptioneng");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Mob1)
                .HasMaxLength(50)
                .HasColumnName("mob1");
            entity.Property(e => e.Mob2)
                .HasMaxLength(50)
                .HasColumnName("mob2");
            entity.Property(e => e.Mob3)
                .HasMaxLength(50)
                .HasColumnName("mob3");
            entity.Property(e => e.Mob4)
                .HasMaxLength(50)
                .HasColumnName("mob4");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.NameEng)
                .HasMaxLength(50)
                .HasColumnName("nameEng");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Web)
                .HasMaxLength(50)
                .HasColumnName("WEB");
        });

        modelBuilder.Entity<QqPurchaseByWajba>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Purchase_By_Wajba");

            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.QttyPurch).HasColumnName("Qtty_Purch");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Smtotal).HasColumnName("SMtotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqPurchaseItemsForDatagridview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Purchase_Items_For_Datagridview");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idd).HasColumnName("idd");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Masraf).HasColumnType("money");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.PuId).HasColumnName("PU_ID");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.TotlPrise).HasColumnType("money");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqReturnByWajba>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Return_By_Wajba");

            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqReturnByWajbaRr>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Return_By_Wajba_RR");

            entity.Property(e => e.QttyReturn).HasColumnName("Qtty_Return");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqReturnFatoraItemsForCountCostumer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Return_Fatora_Items_For_CountCostumer");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Expr1).HasMaxLength(25);
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Hamalya)
                .HasColumnType("money")
                .HasColumnName("hamalya");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idd).HasColumnName("idd");
            entity.Property(e => e.Itemm)
                .HasMaxLength(50)
                .HasColumnName("itemm");
            entity.Property(e => e.Mandob).HasMaxLength(50);
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.PurchasePrise)
                .HasColumnType("money")
                .HasColumnName("Purchase_prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReId).HasColumnName("RE_ID");
            entity.Property(e => e.RePayed).HasColumnType("money");
            entity.Property(e => e.Remaing).HasColumnType("money");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootal).HasColumnType("money");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqReturnItemsForDatagridview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Return_Items_For_Datagridview");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(50)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReId).HasColumnName("RE_ID");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqSalesInvoiceDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Sales_Invoice_Details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqSalesInvoiceDetails2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_Sales_Invoice_Details2");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Smmkaabb).HasColumnName("SMMkaabb");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Summwezn).HasColumnName("SUMMWezn");
            entity.Property(e => e.Sumqtty).HasColumnName("SUMQtty");
        });

        modelBuilder.Entity<QqSalesInvoiceSumm>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_sales_invoice_Summ");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Smmkaabb).HasColumnName("SMMkaabb");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Summwezn).HasColumnName("SUMMWezn");
            entity.Property(e => e.Sumqtty).HasColumnName("SUMQtty");
        });

        modelBuilder.Entity<QqSalesInvoiceSumm2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_sales_invoice_Summ2");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqSubbb>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQ_SUBBB");

            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqqCompanyPurchaseInvoicessForPurchaseManager>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Company_Purchase_invoicess_For_purchase_Manager");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idd).HasColumnName("idd");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.SmTotal)
                .HasColumnType("money")
                .HasColumnName("SM_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqqCountBuyEventItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_BUY_EVENT_ITEMS");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.CostType)
                .HasMaxLength(50)
                .HasColumnName("costType");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
        });

        modelBuilder.Entity<QqqCountBuyEventItemsHoreka>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_BUY_EVENT_ITEMS_HOREKA");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.CostType)
                .HasMaxLength(50)
                .HasColumnName("costType");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
        });

        modelBuilder.Entity<QqqCountBuyEventItemsHorekaNew>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_BUY_EVENT_ITEMS_HOREKA_NEW");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.CostType)
                .HasMaxLength(50)
                .HasColumnName("costType");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
        });

        modelBuilder.Entity<QqqCountCompany>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Company");

            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .HasColumnName("company");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.SmTotal)
                .HasColumnType("money")
                .HasColumnName("SM_Total");
            entity.Property(e => e.Sndtotal)
                .HasColumnType("money")
                .HasColumnName("SNDTotal");
            entity.Property(e => e.SoldTotal)
                .HasColumnType("money")
                .HasColumnName("SOldTotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.TransTotalo).HasColumnType("money");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<QqqCountCompanyPurchaseInvoicess>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Company_Purchase_invoicess");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idd).HasColumnName("idd");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.SmTotal)
                .HasColumnType("money")
                .HasColumnName("SM_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Wajba)
                .HasMaxLength(50)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqqCountCostumer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Costumer");

            entity.Property(e => e.BuyRemain)
                .HasColumnType("money")
                .HasColumnName("Buy_Remain");
            entity.Property(e => e.Costumer)
                .HasMaxLength(50)
                .HasColumnName("costumer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Endaho).HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Pfcost)
                .HasColumnType("money")
                .HasColumnName("PFCost");
            entity.Property(e => e.Ptcost)
                .HasColumnType("money")
                .HasColumnName("PTCost");
            entity.Property(e => e.RetRemain)
                .HasColumnType("money")
                .HasColumnName("Ret_Remain");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
            entity.Property(e => e.Wasil).HasColumnType("money");
        });

        modelBuilder.Entity<QqqCountCostumerBuyFatora>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Costumer_BuyFatora");

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Remain).HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqqCountCostumerNew>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count-Costumer_New");

            entity.Property(e => e.BuRemain)
                .HasColumnType("money")
                .HasColumnName("Bu_Remain");
            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.Costumer)
                .HasMaxLength(50)
                .HasColumnName("costumer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Endaho).HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.PfcostN)
                .HasColumnType("money")
                .HasColumnName("PFCostN");
            entity.Property(e => e.PtcostN)
                .HasColumnType("money")
                .HasColumnName("PTCostN");
            entity.Property(e => e.RettRemain)
                .HasColumnType("money")
                .HasColumnName("Rett_Remain");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
            entity.Property(e => e.Wasil).HasColumnType("money");
        });

        modelBuilder.Entity<QqqCountCostumerNewTRuning>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Costumer_New_T_Runing");

            entity.Property(e => e.BuRemain)
                .HasColumnType("money")
                .HasColumnName("Bu_Remain");
            entity.Property(e => e.Costumer)
                .HasMaxLength(50)
                .HasColumnName("costumer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Endaho).HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.PfcostN)
                .HasColumnType("money")
                .HasColumnName("PFCostN");
            entity.Property(e => e.PtcostN)
                .HasColumnType("money")
                .HasColumnName("PTCostN");
            entity.Property(e => e.RettRemain)
                .HasColumnType("money")
                .HasColumnName("Rett_Remain");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.TransTotalN).HasColumnType("money");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
            entity.Property(e => e.Wasil).HasColumnType("money");
        });

        modelBuilder.Entity<QqqCountCostumerReturnFatora>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Costumer_ReturnFatora");

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.RetRemain)
                .HasColumnType("money")
                .HasColumnName("Ret_Remain");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqqCountIxraci>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_ixraci");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_event");
            entity.Property(e => e.IxraciN)
                .HasMaxLength(50)
                .HasColumnName("ixraciN");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<QqqCountOffice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Endaho)
                .HasColumnType("money")
                .HasColumnName("endaho");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Indana)
                .HasColumnType("money")
                .HasColumnName("indana");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Officee)
                .HasMaxLength(50)
                .HasColumnName("officee");
            entity.Property(e => e.PfrcTotal)
                .HasColumnType("money")
                .HasColumnName("PFRC_Total");
            entity.Property(e => e.PtcTotal)
                .HasColumnType("money")
                .HasColumnName("PTC_Total");
            entity.Property(e => e.PtixTotal)
                .HasColumnType("money")
                .HasColumnName("PTIX_Total");
            entity.Property(e => e.PtoffTotal)
                .HasColumnType("money")
                .HasColumnName("PTOFF_Total");
            entity.Property(e => e.Ptotal).HasColumnType("money");
            entity.Property(e => e.SndTotal)
                .HasColumnType("money")
                .HasColumnName("Snd_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.TransTotalN)
                .HasColumnType("money")
                .HasColumnName("TransTotal_N");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<QqqCountOfficeFromCurrency>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_FROM_Currency");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Office).HasMaxLength(25);
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Tootall).HasColumnType("money");
        });

        modelBuilder.Entity<QqqCountOfficeFromCurrencyDollar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_FROM_Currency_Dollar");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.ExchanFrmTotal).HasColumnType("money");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Office).HasMaxLength(25);
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
        });

        modelBuilder.Entity<QqqCountOfficeFromCurrencyDollarN>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_FROM_Currency_Dollar_N");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Endaho)
                .HasColumnType("money")
                .HasColumnName("endaho");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.ExchanFrmTotal).HasColumnType("money");
            entity.Property(e => e.ExchanTooTotal)
                .HasColumnType("money")
                .HasColumnName("ExchanTOO_Total");
            entity.Property(e => e.Expr1).HasColumnType("money");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Officee)
                .HasMaxLength(50)
                .HasColumnName("officee");
            entity.Property(e => e.PfrcTotal)
                .HasColumnType("money")
                .HasColumnName("PFRC_Total");
            entity.Property(e => e.PtcTotal)
                .HasColumnType("money")
                .HasColumnName("PTC_Total");
            entity.Property(e => e.PtixTotal)
                .HasColumnType("money")
                .HasColumnName("PTIX_Total");
            entity.Property(e => e.PtoffTotal)
                .HasColumnType("money")
                .HasColumnName("PTOFF_Total");
            entity.Property(e => e.Ptotal).HasColumnType("money");
            entity.Property(e => e.SndTotal)
                .HasColumnType("money")
                .HasColumnName("Snd_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotalN)
                .HasColumnType("money")
                .HasColumnName("TransTotal_N");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<QqqCountOfficeMultiCurrency>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Multi_Currency");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Endaho)
                .HasColumnType("money")
                .HasColumnName("endaho");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.ExchanFrmTotal).HasColumnType("money");
            entity.Property(e => e.ExchanTooTotal)
                .HasColumnType("money")
                .HasColumnName("ExchanTOO_Total");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Indana)
                .HasColumnType("money")
                .HasColumnName("indana");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Officee)
                .HasMaxLength(50)
                .HasColumnName("officee");
            entity.Property(e => e.PfrcTotal)
                .HasColumnType("money")
                .HasColumnName("PFRC_Total");
            entity.Property(e => e.PtcTotal)
                .HasColumnType("money")
                .HasColumnName("PTC_Total");
            entity.Property(e => e.PtixTotal)
                .HasColumnType("money")
                .HasColumnName("PTIX_Total");
            entity.Property(e => e.PtoffTotal)
                .HasColumnType("money")
                .HasColumnName("PTOFF_Total");
            entity.Property(e => e.Ptotal).HasColumnType("money");
            entity.Property(e => e.SndTotal)
                .HasColumnType("money")
                .HasColumnName("Snd_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.TransTotalN)
                .HasColumnType("money")
                .HasColumnName("TransTotal_N");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<QqqCountOfficeMultiCurrencyDollar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Multi_Currency_Dollar");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Endaho)
                .HasColumnType("money")
                .HasColumnName("endaho");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.ExchanFrmTotal).HasColumnType("money");
            entity.Property(e => e.ExchanTooTotal)
                .HasColumnType("money")
                .HasColumnName("ExchanTOO_Total");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Indana)
                .HasColumnType("money")
                .HasColumnName("indana");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Officee)
                .HasMaxLength(50)
                .HasColumnName("officee");
            entity.Property(e => e.PfrcTotal)
                .HasColumnType("money")
                .HasColumnName("PFRC_Total");
            entity.Property(e => e.PtcTotal)
                .HasColumnType("money")
                .HasColumnName("PTC_Total");
            entity.Property(e => e.PtixTotal)
                .HasColumnType("money")
                .HasColumnName("PTIX_Total");
            entity.Property(e => e.PtoffTotal)
                .HasColumnType("money")
                .HasColumnName("PTOFF_Total");
            entity.Property(e => e.Ptotal).HasColumnType("money");
            entity.Property(e => e.SndTotal)
                .HasColumnType("money")
                .HasColumnName("Snd_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.TransTotalN)
                .HasColumnType("money")
                .HasColumnName("TransTotal_N");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<QqqCountOfficeMultiCurrencyDollarNn>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Multi_Currency_Dollar_NN");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Endaho)
                .HasColumnType("money")
                .HasColumnName("endaho");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.ExchanFrmTotal).HasColumnType("money");
            entity.Property(e => e.ExchanTooTotal)
                .HasColumnType("money")
                .HasColumnName("ExchanTOO_Total");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Indana)
                .HasColumnType("money")
                .HasColumnName("indana");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Officee)
                .HasMaxLength(50)
                .HasColumnName("officee");
            entity.Property(e => e.PfrcTotal)
                .HasColumnType("money")
                .HasColumnName("PFRC_Total");
            entity.Property(e => e.PtcTotal)
                .HasColumnType("money")
                .HasColumnName("PTC_Total");
            entity.Property(e => e.PtixTotal)
                .HasColumnType("money")
                .HasColumnName("PTIX_Total");
            entity.Property(e => e.PtoffTotal)
                .HasColumnType("money")
                .HasColumnName("PTOFF_Total");
            entity.Property(e => e.Ptotal).HasColumnType("money");
            entity.Property(e => e.SndTotal)
                .HasColumnType("money")
                .HasColumnName("Snd_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.TransTotalN)
                .HasColumnType("money")
                .HasColumnName("TransTotal_N");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<QqqCountOfficeMultiCurrencyIqd>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Multi_Currency_IQD");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Endaho)
                .HasColumnType("money")
                .HasColumnName("endaho");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.ExchanFrmTotal).HasColumnType("money");
            entity.Property(e => e.ExchanTooTotal)
                .HasColumnType("money")
                .HasColumnName("ExchanTOO_Total");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Indana)
                .HasColumnType("money")
                .HasColumnName("indana");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Officee)
                .HasMaxLength(50)
                .HasColumnName("officee");
            entity.Property(e => e.PfrcTotalIqd)
                .HasColumnType("money")
                .HasColumnName("PFRC_Total_IQD");
            entity.Property(e => e.PtcTotal)
                .HasColumnType("money")
                .HasColumnName("PTC_Total");
            entity.Property(e => e.PtixTotal)
                .HasColumnType("money")
                .HasColumnName("PTIX_Total");
            entity.Property(e => e.PtoffTotal)
                .HasColumnType("money")
                .HasColumnName("PTOFF_Total");
            entity.Property(e => e.Ptotal).HasColumnType("money");
            entity.Property(e => e.SndTotal)
                .HasColumnType("money")
                .HasColumnName("Snd_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.TransTotalN)
                .HasColumnType("money")
                .HasColumnName("TransTotal_N");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
        });

        modelBuilder.Entity<QqqCountOfficePayFrmCostumer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_PayFrmCostumer");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PayType).HasMaxLength(50);
            entity.Property(e => e.PfrcTotal)
                .HasColumnType("money")
                .HasColumnName("PFRC_Total");
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.ToOffice)
                .HasMaxLength(50)
                .HasColumnName("To_Office");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficePayFrmCostumerDollar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_PayFrmCostumer_Dollar");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PayType).HasMaxLength(50);
            entity.Property(e => e.PfrcTotal)
                .HasColumnType("money")
                .HasColumnName("PFRC_Total");
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.ToOffice)
                .HasMaxLength(50)
                .HasColumnName("To_Office");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficePayFrmCostumerIqd>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_PayFrmCostumer_IQD");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PayType).HasMaxLength(50);
            entity.Property(e => e.PfrcTotalIqd)
                .HasColumnType("money")
                .HasColumnName("PFRC_Total_IQD");
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.ToOffice)
                .HasMaxLength(50)
                .HasColumnName("To_Office");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficePayFromOfficeDollar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Pay_From_Office_Dollar");

            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Offiice)
                .HasMaxLength(50)
                .HasColumnName("offiice");
            entity.Property(e => e.Ptotal).HasColumnType("money");
            entity.Property(e => e.PtotalL).HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
        });

        modelBuilder.Entity<QqqCountOfficePayToCostumer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Pay_ToCostumer");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PtcTotal)
                .HasColumnType("money")
                .HasColumnName("PTC_Total");
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.ToOffice)
                .HasMaxLength(50)
                .HasColumnName("To_Office");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Typee)
                .HasMaxLength(10)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficePayToCostumerDollar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Pay_ToCostumer_Dollar");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PtcTotal)
                .HasColumnType("money")
                .HasColumnName("PTC_Total");
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.ToOffice)
                .HasMaxLength(50)
                .HasColumnName("To_Office");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Typee)
                .HasMaxLength(10)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficePayToIxraci>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_PayToIxraci");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.FrmOffice)
                .HasMaxLength(50)
                .HasColumnName("Frm_Office");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IxraciN)
                .HasMaxLength(50)
                .HasColumnName("ixraciN");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PtixTotal)
                .HasColumnType("money")
                .HasColumnName("PTIX_Total");
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Typee)
                .HasMaxLength(10)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficePayToIxraciDollar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_PayToIxraci_Dollar");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.FrmOffice)
                .HasMaxLength(50)
                .HasColumnName("Frm_Office");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IxraciN)
                .HasMaxLength(50)
                .HasColumnName("ixraciN");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.PtixTotal)
                .HasColumnType("money")
                .HasColumnName("PTIX_Total");
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Typee)
                .HasMaxLength(10)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficePaytoOffice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_PaytoOffice");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Dolar).HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IqdTtal)
                .HasColumnType("money")
                .HasColumnName("IQD_Ttal");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Offiice)
                .HasMaxLength(50)
                .HasColumnName("offiice");
            entity.Property(e => e.PtoffTotal)
                .HasColumnType("money")
                .HasColumnName("PTOFF_Total");
            entity.Property(e => e.Ptotal).HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficePaytoOfficeDollar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_PaytoOffice_Dollar");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Dolar).HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IqdTtal)
                .HasColumnType("money")
                .HasColumnName("IQD_Ttal");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Offiice)
                .HasMaxLength(50)
                .HasColumnName("offiice");
            entity.Property(e => e.PtoffTotal)
                .HasColumnType("money")
                .HasColumnName("PTOFF_Total");
            entity.Property(e => e.Ptotal).HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficeSendMoney>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Send_Money");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Amola)
                .HasColumnType("money")
                .HasColumnName("amola");
            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .HasColumnName("company");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Offiice)
                .HasMaxLength(50)
                .HasColumnName("offiice");
            entity.Property(e => e.Run).HasColumnName("run");
            entity.Property(e => e.SndTotal)
                .HasColumnType("money")
                .HasColumnName("Snd_Total");
            entity.Property(e => e.SuTotal)
                .HasColumnType("money")
                .HasColumnName("SU_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficeSendMoneyDollar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Send_Money_Dollar");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Amola)
                .HasColumnType("money")
                .HasColumnName("amola");
            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .HasColumnName("company");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Offiice)
                .HasMaxLength(50)
                .HasColumnName("offiice");
            entity.Property(e => e.Run).HasColumnName("run");
            entity.Property(e => e.SndTotal)
                .HasColumnType("money")
                .HasColumnName("Snd_Total");
            entity.Property(e => e.SuTotal)
                .HasColumnType("money")
                .HasColumnName("SU_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqCountOfficeTooCurrency>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Too_Currency");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventIdOther)
                .HasMaxLength(100)
                .HasColumnName("event_id_Other");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.OfficeOther)
                .HasMaxLength(25)
                .HasColumnName("Office_Other");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.SymbolL).HasMaxLength(50);
            entity.Property(e => e.TotaLlafter)
                .HasColumnType("money")
                .HasColumnName("totaLLAfter");
        });

        modelBuilder.Entity<QqqCountOfficeTooCurrencyDollar>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Office_Too_Currency_Dollar");

            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventIdOther)
                .HasMaxLength(100)
                .HasColumnName("event_id_Other");
            entity.Property(e => e.ExchanTooTotal)
                .HasColumnType("money")
                .HasColumnName("ExchanTOO_Total");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("note");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.OfficeOther)
                .HasMaxLength(25)
                .HasColumnName("Office_Other");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.SymbolL).HasMaxLength(50);
        });

        modelBuilder.Entity<QqqCountPerson>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Count_Person");

            entity.Property(e => e.Costumer)
                .HasMaxLength(50)
                .HasColumnName("costumer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Endaho).HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.MtabqaDate).HasColumnName("mtabqaDate");
            entity.Property(e => e.MtabqaDatee)
                .HasMaxLength(50)
                .HasColumnName("mtabqaDatee");
            entity.Property(e => e.Notess)
                .HasColumnType("ntext")
                .HasColumnName("notess");
            entity.Property(e => e.Noww)
                .HasColumnType("datetime")
                .HasColumnName("noww");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Trans).HasColumnName("trans");
            entity.Property(e => e.TransTotal).HasColumnType("money");
            entity.Property(e => e.TransTotalO).HasColumnType("money");
            entity.Property(e => e.Ttttotal)
                .HasColumnType("money")
                .HasColumnName("TTTtotal");
            entity.Property(e => e.Typeevent)
                .HasMaxLength(50)
                .HasColumnName("typeevent");
            entity.Property(e => e.Wasil).HasColumnType("money");
        });

        modelBuilder.Entity<QqqOrderItemManager>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Order_Item_Manager");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.BuId).HasColumnName("BU_ID");
            entity.Property(e => e.Checkeed).HasColumnName("checkeed");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
        });

        modelBuilder.Entity<QqqPrintPayToCostomer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Print_Pay_To_Costomer");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LasttRemain).HasColumnType("money");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Runn).HasColumnName("runn");
            entity.Property(e => e.Safiremain)
                .HasColumnType("money")
                .HasColumnName("SAFIREMAIN");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.ToOffice)
                .HasMaxLength(50)
                .HasColumnName("To_Office");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Typee)
                .HasMaxLength(10)
                .HasColumnName("typee");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<QqqPurchaseInvoiceSumm>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Purchase_Invoice_Summ");

            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SmTotal)
                .HasColumnType("money")
                .HasColumnName("SM_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqqQtty>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQ_Qtty");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Secode).HasColumnName("secode");
        });

        modelBuilder.Entity<QqqqCountCompanyAllRemain>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQQ_Count_Company_All_Remain");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .HasColumnName("company");
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Remainn).HasColumnType("money");
            entity.Property(e => e.Smtotal)
                .HasColumnType("money")
                .HasColumnName("SMTotal");
            entity.Property(e => e.Sndtotal)
                .HasColumnType("money")
                .HasColumnName("SNDTotal");
            entity.Property(e => e.SoldTotal)
                .HasColumnType("money")
                .HasColumnName("SOldTotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqqqCountCompanyPurchase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQQ_Count_Company_Purchase");

            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Smtotal)
                .HasColumnType("money")
                .HasColumnName("SMTotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqqqCountCompanyRemainingOld>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQQ_Count_Company_RemainingOld");

            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.SoldTotal)
                .HasColumnType("money")
                .HasColumnName("SOldTotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqqqCountCompanyRemainingOldCountt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQQ_Count_Company_RemainingOld_Countt");

            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SoldTotal)
                .HasColumnType("money")
                .HasColumnName("SOldTotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqqqCountCompanySendMoney>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQQ_Count_Company_Send_Money");

            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .HasColumnName("company");
            entity.Property(e => e.Sndtotal)
                .HasColumnType("money")
                .HasColumnName("SNDTotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<QqqqCountCompanySendMoneyCountt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("QQQQ_Count_Company_Send_Money_Countt");

            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .HasColumnName("company");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sndtotal)
                .HasColumnType("money")
                .HasColumnName("SNDTotal");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<ReName>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Re_Name");

            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .HasColumnName("adress");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Mobilee)
                .HasMaxLength(25)
                .HasColumnName("mobilee");
            entity.Property(e => e.ReName1)
                .HasMaxLength(255)
                .HasColumnName("Re_Name");
        });

        modelBuilder.Entity<ReturnFatora>(entity =>
        {
            entity.ToTable("ReturnFatora");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CarNo)
                .HasMaxLength(50)
                .HasColumnName("carNo");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Datee).HasColumnName("datee");
            entity.Property(e => e.Dolar).HasDefaultValue(0.0);
            entity.Property(e => e.Driver).HasMaxLength(50);
            entity.Property(e => e.DriverMob)
                .HasMaxLength(50)
                .HasColumnName("driverMob");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Hamalya)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("hamalya");
            entity.Property(e => e.Idd)
                .HasDefaultValue(0)
                .HasColumnName("idd");
            entity.Property(e => e.ManCcount)
                .HasDefaultValue(0.0)
                .HasColumnName("man_ccount");
            entity.Property(e => e.Mandob).HasMaxLength(50);
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.RePayed)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Remaing)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Tootal)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<ReturnFatoraItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ReturnFatora_items");

            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Countt).HasDefaultValue(0.0);
            entity.Property(e => e.Factoryy)
                .HasMaxLength(50)
                .HasColumnName("factoryy");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.PurchasePrise)
                .HasColumnType("money")
                .HasColumnName("Purchase_prise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.QttRemaining)
                .HasDefaultValue(0.0)
                .HasColumnName("Qtt_Remaining");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0.0)
                .HasComment("Qtt_Remaining")
                .HasColumnName("quantity");
            entity.Property(e => e.ReId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RE_ID");
            entity.Property(e => e.Rub7Karton)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("Rub7_karton");
            entity.Property(e => e.Secode)
                .HasDefaultValue(0)
                .HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Typee)
                .HasMaxLength(50)
                .HasColumnName("typee");
            entity.Property(e => e.Wajba)
                .HasMaxLength(255)
                .HasColumnName("wajba");
            entity.Property(e => e.Weznn).HasDefaultValue(0f);
        });

        modelBuilder.Entity<RewardCustomerLinker>(entity =>
        {
            entity.ToTable("RewardCustomerLinker");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SecondryCategory>(entity =>
        {
            entity.ToTable("SecondryCategory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NameAr).HasColumnName("NameAR");
            entity.Property(e => e.NameEn).HasColumnName("NameEN");
            entity.Property(e => e.NameKr).HasColumnName("NameKR");
            entity.Property(e => e.NameTr).HasColumnName("NameTR");
        });

        modelBuilder.Entity<SendMoney>(entity =>
        {
            entity.ToTable("sendMoney");

            entity.Property(e => e.Id)
                .HasDefaultValue(1)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Amola)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("amola");
            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .HasColumnName("company");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DecreaseFrom)
                .HasMaxLength(50)
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.Dolar)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("event_id");
            entity.Property(e => e.Iqd)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("IQD");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("notes");
            entity.Property(e => e.Now)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.Offiice)
                .HasMaxLength(50)
                .HasColumnName("offiice");
            entity.Property(e => e.Run)
                .HasDefaultValue(false)
                .HasColumnName("run");
            entity.Property(e => e.SuTotal)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("SU_Total");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.Treasurer)
                .HasMaxLength(50)
                .HasDefaultValue("Treasurer");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<SeriesOrCollectionOrCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SeriesOrCollection");

            entity.ToTable("SeriesOrCollectionOrCategory");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Catalog).WithMany(p => p.SeriesOrCollectionOrCategories)
                .HasForeignKey(d => d.CatalogId)
                .HasConstraintName("FK_SeriesOrCollectionOrCategory_Catalog");
        });

        modelBuilder.Entity<Specification>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DescriptionAr).HasMaxLength(80);
            entity.Property(e => e.DescriptionEn).HasMaxLength(80);
            entity.Property(e => e.DescriptionKr).HasMaxLength(80);
            entity.Property(e => e.DescriptionTr).HasMaxLength(80);
            entity.Property(e => e.TitelAr).HasMaxLength(80);
            entity.Property(e => e.TitelEn).HasMaxLength(80);
            entity.Property(e => e.TitleKr).HasMaxLength(80);
            entity.Property(e => e.TitleTr).HasMaxLength(80);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_State");

            entity.ToTable("Status");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.StateAr).HasMaxLength(50);
            entity.Property(e => e.StateEn).HasMaxLength(50);
        });

        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SystemSetting");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Descriptioneng)
                .HasMaxLength(255)
                .HasColumnName("descriptioneng");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Mob1)
                .HasMaxLength(50)
                .HasColumnName("mob1");
            entity.Property(e => e.Mob2)
                .HasMaxLength(50)
                .HasColumnName("mob2");
            entity.Property(e => e.Mob3)
                .HasMaxLength(50)
                .HasColumnName("mob3");
            entity.Property(e => e.Mob4)
                .HasMaxLength(50)
                .HasColumnName("mob4");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.NameEng)
                .HasMaxLength(50)
                .HasColumnName("nameEng");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Web)
                .HasMaxLength(50)
                .HasColumnName("WEB");
        });

        modelBuilder.Entity<sacmy.Server.Models.Task>(entity =>
        {
            entity.ToTable("Task");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.AssignedToEmployeeNavigation).WithMany(p => p.TaskAssignedToEmployeeNavigations)
                .HasForeignKey(d => d.AssignedToEmployee)
                .HasConstraintName("FK_Task_Employee");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TaskCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Task_Employee1");

            entity.HasOne(d => d.Status).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Task_Status");
        });

        modelBuilder.Entity<TaskNote>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TaskNotes)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskNotes_Employee");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskNotes)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaskNotes_Task");
        });

        modelBuilder.Entity<TblLevel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Levels");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Levels).HasMaxLength(50);
        });

        modelBuilder.Entity<TblTutriolVed>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Tutriol_VED");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Titlee).HasMaxLength(350);
            entity.Property(e => e.Urll)
                .HasMaxLength(350)
                .HasColumnName("URLL");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TblUser");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Password).HasMaxLength(25);
            entity.Property(e => e.Per)
                .HasMaxLength(25)
                .HasColumnName("per");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Username).HasMaxLength(25);
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Tracks_customer");

            entity.HasOne(d => d.Employe).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.EmployeId)
                .HasConstraintName("FK_Tracks_Employee");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_Tracks_BuyFatora");

            entity.HasOne(d => d.Type).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Tracks_TrackType");
        });

        modelBuilder.Entity<TrackComment>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.ReOpenAt).HasColumnType("datetime");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.TrackCommentAssignedToNavigations)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK_TrackComments_Employee1");

            entity.HasOne(d => d.Employee).WithMany(p => p.TrackCommentEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_TrackComments_Employee");

            entity.HasOne(d => d.Track).WithMany(p => p.TrackComments)
                .HasForeignKey(d => d.TrackId)
                .HasConstraintName("FK_TrackComments_Tracks");
        });

        modelBuilder.Entity<TrackCommentState>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.State).WithMany(p => p.TrackCommentStates)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_TrackCommentStates_State");

            entity.HasOne(d => d.TrackComment).WithMany(p => p.TrackCommentStates)
                .HasForeignKey(d => d.TrackCommentId)
                .HasConstraintName("FK_TrackCommentStates_TrackComments");
        });

        modelBuilder.Entity<TrackType>(entity =>
        {
            entity.ToTable("TrackType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TypeAr).HasMaxLength(50);
            entity.Property(e => e.TypeEn).HasMaxLength(50);
        });

        modelBuilder.Entity<Typee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("typee");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Typee1)
                .HasMaxLength(50)
                .HasColumnName("typee");
        });

        modelBuilder.Entity<UnavilableOrderedItem>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BillId).HasColumnName("billId");
            entity.Property(e => e.PattrenCode).HasMaxLength(12);
            entity.Property(e => e.Sku).HasMaxLength(12);
        });

        modelBuilder.Entity<Userlogin>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("userlogin");

            entity.Property(e => e.Fulldate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Per)
                .HasMaxLength(25)
                .HasColumnName("per");
            entity.Property(e => e.Username).HasMaxLength(25);
        });

        modelBuilder.Entity<View1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_1");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.BuyRemain)
                .HasColumnType("money")
                .HasColumnName("Buy_Remain");
            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Mob)
                .HasMaxLength(50)
                .HasColumnName("mob");
            entity.Property(e => e.Pftotal)
                .HasColumnType("money")
                .HasColumnName("PFTotal");
            entity.Property(e => e.Pttotal)
                .HasColumnType("money")
                .HasColumnName("PTTotal");
            entity.Property(e => e.RetRemain)
                .HasColumnType("money")
                .HasColumnName("Ret_Remain");
            entity.Property(e => e.Safiremain)
                .HasColumnType("money")
                .HasColumnName("SAFIREMAIN");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<WwLastCompanySecode>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("WW_Last_Company_Secode");

            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Codd)
                .HasMaxLength(50)
                .HasColumnName("codd");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Prise)
                .HasColumnType("money")
                .HasColumnName("prise");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<WwLastCompanySecodeForOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("WW_Last_Company_Secode_For_Orders");

            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Itemm)
                .HasMaxLength(250)
                .HasColumnName("itemm");
            entity.Property(e => e.Lprise)
                .HasColumnType("money")
                .HasColumnName("LPrise");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<WwOrderByCompanySecodeReady>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("WW_Order_BY_Company_Secode_Ready");

            entity.Property(e => e.Barcod)
                .HasMaxLength(50)
                .HasColumnName("barcod");
            entity.Property(e => e.BoxContain).HasMaxLength(25);
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Cod)
                .HasMaxLength(50)
                .HasColumnName("cod");
            entity.Property(e => e.CodIqd)
                .HasMaxLength(25)
                .HasColumnName("codIQD");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Factory)
                .HasMaxLength(50)
                .HasColumnName("factory");
            entity.Property(e => e.Item)
                .HasMaxLength(250)
                .HasColumnName("item");
            entity.Property(e => e.Lprise)
                .HasColumnType("money")
                .HasColumnName("LPrise");
            entity.Property(e => e.QiyasUnit)
                .HasMaxLength(50)
                .HasColumnName("qiyas_unit");
            entity.Property(e => e.Secode).HasColumnName("secode");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        modelBuilder.Entity<WwwHorekaRetailName>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("WWW_Horeka_Retail_Names");

            entity.Property(e => e.CostType)
                .HasMaxLength(50)
                .HasColumnName("costType");
            entity.Property(e => e.Subb)
                .HasMaxLength(25)
                .HasColumnName("subb");
        });

        modelBuilder.Entity<ZzOnlineOrderTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ZZ_Online_Order_Total");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Sub).HasMaxLength(50);
            entity.Property(e => e.Total).HasColumnType("money");
        });

        modelBuilder.Entity<ZzOrderOnlinePrint>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ZZ_Order_Online_Print");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Barcod).HasMaxLength(50);
            entity.Property(e => e.Checked).HasColumnName("checked");
            entity.Property(e => e.CostumerName)
                .HasMaxLength(50)
                .HasColumnName("Costumer_Name");
            entity.Property(e => e.Item).HasMaxLength(250);
            entity.Property(e => e.MoldNo).HasMaxLength(50);
            entity.Property(e => e.Note).HasMaxLength(350);
            entity.Property(e => e.Now)
                .HasColumnType("datetime")
                .HasColumnName("now");
            entity.Property(e => e.OrR).HasColumnName("Or_R");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Secod)
                .HasMaxLength(50)
                .HasColumnName("secod");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("SKU");
            entity.Property(e => e.Sub).HasMaxLength(50);
            entity.Property(e => e.SumTotal).HasColumnType("money");
            entity.Property(e => e.Total).HasColumnType("money");
            entity.Property(e => e.Treasurer).HasMaxLength(50);
            entity.Property(e => e.Uuser)
                .HasMaxLength(50)
                .HasColumnName("uuser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
