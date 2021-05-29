IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Approvers] (
        [Id] int NOT NULL IDENTITY,
        [ApproverRoleId] int NOT NULL,
        [ApproverType] int NOT NULL,
        CONSTRAINT [PK_Approvers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserType] int NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Banks] (
        [Id] int NOT NULL IDENTITY,
        [BankCode] nvarchar(max) NULL,
        [BankName] nvarchar(max) NULL,
        CONSTRAINT [PK_Banks] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Departments] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Departments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [EmployeeTypes] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_EmployeeTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Loans] (
        [Id] int NOT NULL IDENTITY,
        [LoanCode] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Active] bit NOT NULL,
        [LoanType] int NOT NULL,
        CONSTRAINT [PK_Loans] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [MemberTypes] (
        [Id] int NOT NULL IDENTITY,
        [description] nvarchar(max) NULL,
        CONSTRAINT [PK_MemberTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [ModeOfPayments] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_ModeOfPayments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Modules] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Modules] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Positions] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Positions] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [States] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_States] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [TargetLoanTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_TargetLoanTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [UserTypes] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_UserTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [ConcurrentLoans] (
        [Id] int NOT NULL IDENTITY,
        [LoanId] int NOT NULL,
        [ConcurrentLoanId] int NULL,
        CONSTRAINT [PK_ConcurrentLoans] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ConcurrentLoans_Loans_LoanId] FOREIGN KEY ([LoanId]) REFERENCES [Loans] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [LoanConfigs] (
        [Id] int NOT NULL IDENTITY,
        [MinLoanAmount] decimal(10,2) NOT NULL,
        [MaxLoanAmount] decimal(10,2) NOT NULL,
        [MonthlyRepayPeriod] int NOT NULL,
        [MinMonthlyRepayPeriod] int NOT NULL,
        [MaxMonthlyRepayPeriod] int NOT NULL,
        [IntrestRate] real NOT NULL,
        [LumpSumSavingsAmount] decimal(10,2) NOT NULL,
        [IsATargetLoan] bit NOT NULL,
        [MonthlySavingsAmount] decimal(10,2) NOT NULL,
        [ExistingLoanFeeAmount] decimal(10,2) NOT NULL,
        [WaitingPeriod] real NOT NULL,
        [PeriodBeforeOffset] int NOT NULL,
        [AllowPartialOffset] bit NOT NULL,
        [AdminChargeType] int NOT NULL,
        [AdminChargeAmount] real NOT NULL,
        [LoanId] int NOT NULL,
        [MemberTypeId] int NOT NULL,
        [AllowConcurent] bit NOT NULL,
        [ConcurrentQualifyingPeriods] int NOT NULL,
        CONSTRAINT [PK_LoanConfigs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LoanConfigs_Loans_LoanId] FOREIGN KEY ([LoanId]) REFERENCES [Loans] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_LoanConfigs_MemberTypes_MemberTypeId] FOREIGN KEY ([MemberTypeId]) REFERENCES [MemberTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [LoanOffsetModeOfPayments] (
        [Id] int NOT NULL IDENTITY,
        [LoanId] int NOT NULL,
        [ModeOfPaymentId] int NOT NULL,
        CONSTRAINT [PK_LoanOffsetModeOfPayments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LoanOffsetModeOfPayments_Loans_LoanId] FOREIGN KEY ([LoanId]) REFERENCES [Loans] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_LoanOffsetModeOfPayments_ModeOfPayments_ModeOfPaymentId] FOREIGN KEY ([ModeOfPaymentId]) REFERENCES [ModeOfPayments] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [ModuleApprovers] (
        [Id] int NOT NULL IDENTITY,
        [ModuleId] int NOT NULL,
        [ApproverId] int NOT NULL,
        [Level] int NOT NULL,
        CONSTRAINT [PK_ModuleApprovers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ModuleApprovers_Approvers_ApproverId] FOREIGN KEY ([ApproverId]) REFERENCES [Approvers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ModuleApprovers_Modules_ModuleId] FOREIGN KEY ([ModuleId]) REFERENCES [Modules] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Persons] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [MiddleName] nvarchar(max) NULL,
        [Sex] nvarchar(max) NULL,
        [DateOfBirth] datetime2 NOT NULL,
        [Title] nvarchar(max) NULL,
        [WorkPhone] nvarchar(max) NULL,
        [MobileNumber] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Address1] nvarchar(max) NULL,
        [Address2] nvarchar(max) NULL,
        [StateId] int NOT NULL,
        [Country] nvarchar(max) NULL,
        [MaritalStatus] nvarchar(max) NULL,
        [DateCreated] datetime2 NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [LastModifiedDate] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_Persons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Persons_States_StateId] FOREIGN KEY ([StateId]) REFERENCES [States] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Employees] (
        [Id] int NOT NULL IDENTITY,
        [PersonId] int NOT NULL,
        [JobTitle] nvarchar(max) NULL,
        [DepartmentId] int NOT NULL,
        [StateOfOriginId] int NOT NULL,
        [EmployeeTypeId] int NOT NULL,
        [DateOfHire] datetime2 NOT NULL,
        [ResignationDate] datetime2 NOT NULL,
        [AnnualSalary] decimal(10,2) NOT NULL,
        [BasicSalary] decimal(10,2) NOT NULL,
        [Active] bit NOT NULL,
        [Deleted] bit NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [LastModifiedDate] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Employees_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Employees_EmployeeTypes_EmployeeTypeId] FOREIGN KEY ([EmployeeTypeId]) REFERENCES [EmployeeTypes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Employees_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Members] (
        [Id] int NOT NULL IDENTITY,
        [PersonId] int NOT NULL,
        [EmployeeNumber] nvarchar(max) NULL,
        [MemberType] int NOT NULL,
        [UserId] nvarchar(max) NULL,
        [RegistrationDate] datetime2 NOT NULL,
        [Active] bit NOT NULL,
        [Deleted] bit NOT NULL,
        CONSTRAINT [PK_Members] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Members_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [NextOfKins] (
        [Id] int NOT NULL IDENTITY,
        [PersonId] int NOT NULL,
        [Category] nvarchar(max) NULL,
        [ParentPersonId] int NOT NULL,
        CONSTRAINT [PK_NextOfKins] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_NextOfKins_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Beneficiaries] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] int NOT NULL,
        [BankId] int NOT NULL,
        [AccountNumber] nvarchar(max) NULL,
        [AccountName] nvarchar(max) NULL,
        [Primary] bit NOT NULL,
        CONSTRAINT [PK_Beneficiaries] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Beneficiaries_Banks_BankId] FOREIGN KEY ([BankId]) REFERENCES [Banks] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Beneficiaries_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE TABLE [Executives] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] int NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [PositionId] int NOT NULL,
        CONSTRAINT [PK_Executives] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Executives_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Executives_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Positions] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_Beneficiaries_BankId] ON [Beneficiaries] ([BankId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_Beneficiaries_MemberId] ON [Beneficiaries] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_ConcurrentLoans_LoanId] ON [ConcurrentLoans] ([LoanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_Employees_DepartmentId] ON [Employees] ([DepartmentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_Employees_EmployeeTypeId] ON [Employees] ([EmployeeTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_Employees_PersonId] ON [Employees] ([PersonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_Executives_MemberId] ON [Executives] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_Executives_PositionId] ON [Executives] ([PositionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_LoanConfigs_LoanId] ON [LoanConfigs] ([LoanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_LoanConfigs_MemberTypeId] ON [LoanConfigs] ([MemberTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_LoanOffsetModeOfPayments_LoanId] ON [LoanOffsetModeOfPayments] ([LoanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_LoanOffsetModeOfPayments_ModeOfPaymentId] ON [LoanOffsetModeOfPayments] ([ModeOfPaymentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_Members_PersonId] ON [Members] ([PersonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_ModuleApprovers_ApproverId] ON [ModuleApprovers] ([ApproverId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_ModuleApprovers_ModuleId] ON [ModuleApprovers] ([ModuleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_NextOfKins_PersonId] ON [NextOfKins] ([PersonId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    CREATE INDEX [IX_Persons_StateId] ON [Persons] ([StateId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210126174345_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210126174345_InitialCreate', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    EXEC sp_rename N'[MemberTypes].[description]', N'Description', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [AccountTypes] (
        [Id] int NOT NULL IDENTITY,
        [Category] nvarchar(max) NULL,
        CONSTRAINT [PK_AccountTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [CreditCommittees] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] int NOT NULL,
        [PositionId] int NOT NULL,
        CONSTRAINT [PK_CreditCommittees] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CreditCommittees_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CreditCommittees_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Positions] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [LoanApplications] (
        [Id] int NOT NULL IDENTITY,
        [LoanId] int NOT NULL,
        [MemberId] int NOT NULL,
        [DateSubmitted] datetime2 NOT NULL,
        [LoanAmount] decimal(10,2) NOT NULL,
        [RepaymentPeriod] int NOT NULL,
        [Principal] decimal(10,2) NOT NULL,
        [Status] int NOT NULL,
        [Intrest] real NOT NULL,
        [EffectiveDate] datetime2 NULL,
        [MethodOfCollectionId] int NOT NULL,
        [ApprovalCount] int NOT NULL,
        CONSTRAINT [PK_LoanApplications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LoanApplications_Loans_LoanId] FOREIGN KEY ([LoanId]) REFERENCES [Loans] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_LoanApplications_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [LoanBeneficiaries] (
        [Id] int NOT NULL IDENTITY,
        [LoanApplicationId] int NOT NULL,
        [BeneficiaryId] int NOT NULL,
        CONSTRAINT [PK_LoanBeneficiaries] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LoanBeneficiaries_Beneficiaries_BeneficiaryId] FOREIGN KEY ([BeneficiaryId]) REFERENCES [Beneficiaries] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [MemberSavings] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] int NOT NULL,
        [SavingsAmount] decimal(10,2) NOT NULL,
        [Type] int NOT NULL,
        CONSTRAINT [PK_MemberSavings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MemberSavings_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [MethodOfCollections] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_MethodOfCollections] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [PendingApprovals] (
        [Id] int NOT NULL IDENTITY,
        [ModuleId] int NOT NULL,
        [ItemId] int NOT NULL,
        [ApproverId] int NOT NULL,
        CONSTRAINT [PK_PendingApprovals] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PendingApprovals_Approvers_ApproverId] FOREIGN KEY ([ApproverId]) REFERENCES [Approvers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PendingApprovals_Modules_ModuleId] FOREIGN KEY ([ModuleId]) REFERENCES [Modules] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [RegistrationFees] (
        [Id] int NOT NULL IDENTITY,
        [MemberTypeId] int NOT NULL,
        [Amount] decimal(10,2) NOT NULL,
        CONSTRAINT [PK_RegistrationFees] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [SavingDepositLedgers] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] int NOT NULL,
        [SavingsType] int NOT NULL,
        [TransactionDate] datetime2 NOT NULL,
        [PreviousBalance] decimal(10,2) NOT NULL,
        [DepositAmount] decimal(10,2) NOT NULL,
        [CurrentBalance] decimal(10,2) NOT NULL,
        [Status] int NOT NULL,
        [TrancastionTypeId] int NOT NULL,
        CONSTRAINT [PK_SavingDepositLedgers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SavingDepositLedgers_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [SavingDepositTransactions] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] int NOT NULL,
        [SavingsType] int NOT NULL,
        [TransactionDate] datetime2 NOT NULL,
        [DepositAmount] decimal(10,2) NOT NULL,
        [ApprovalCount] int NOT NULL,
        [Status] int NOT NULL,
        [TransactionTypeId] int NOT NULL,
        [LastApprovalDate] datetime2 NOT NULL,
        [LastApprovedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_SavingDepositTransactions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SavingDepositTransactions_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [SavingsConfigurations] (
        [Id] int NOT NULL IDENTITY,
        [MemberTypeId] int NOT NULL,
        [MinimumSavingsAmount] decimal(10,2) NOT NULL,
        CONSTRAINT [PK_SavingsConfigurations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SavingsConfigurations_MemberTypes_MemberTypeId] FOREIGN KEY ([MemberTypeId]) REFERENCES [MemberTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [TransactionTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_TransactionTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [VoucherTransactionDetails] (
        [Id] int NOT NULL IDENTITY,
        [VoucherTransactionId] int NOT NULL,
        [AccountId] int NOT NULL,
        [Amount] decimal(10,2) NOT NULL,
        [Narration] nvarchar(max) NULL,
        CONSTRAINT [PK_VoucherTransactionDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [Accounts] (
        [Id] int NOT NULL IDENTITY,
        [AccountTypeId] int NOT NULL,
        [Code] nvarchar(max) NULL,
        [SageCode] nvarchar(max) NULL,
        CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Accounts_AccountTypes_AccountTypeId] FOREIGN KEY ([AccountTypeId]) REFERENCES [AccountTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [LoanRepayments] (
        [Id] int NOT NULL IDENTITY,
        [LoanApplicationId] int NOT NULL,
        [MemberId] int NOT NULL,
        [RepaymentDte] datetime2 NOT NULL,
        [Principal] decimal(10,2) NOT NULL,
        [Intrest] real NOT NULL,
        [Fees] decimal(10,2) NOT NULL,
        [TotalPayment] decimal(10,2) NOT NULL,
        [Paid] bit NULL,
        [DatePaid] datetime2 NULL,
        CONSTRAINT [PK_LoanRepayments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LoanRepayments_LoanApplications_LoanApplicationId] FOREIGN KEY ([LoanApplicationId]) REFERENCES [LoanApplications] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE TABLE [VoucherTransactions] (
        [Id] int NOT NULL IDENTITY,
        [VoucherNumber] nvarchar(max) NULL,
        [VoucherDate] datetime2 NOT NULL,
        [Type] int NOT NULL,
        [TransactionDate] datetime2 NOT NULL,
        [TransactionTypeId] int NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_VoucherTransactions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_VoucherTransactions_TransactionTypes_TransactionTypeId] FOREIGN KEY ([TransactionTypeId]) REFERENCES [TransactionTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_Accounts_AccountTypeId] ON [Accounts] ([AccountTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_CreditCommittees_MemberId] ON [CreditCommittees] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_CreditCommittees_PositionId] ON [CreditCommittees] ([PositionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_LoanApplications_LoanId] ON [LoanApplications] ([LoanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_LoanApplications_MemberId] ON [LoanApplications] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_LoanBeneficiaries_BeneficiaryId] ON [LoanBeneficiaries] ([BeneficiaryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_LoanRepayments_LoanApplicationId] ON [LoanRepayments] ([LoanApplicationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_MemberSavings_MemberId] ON [MemberSavings] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_PendingApprovals_ApproverId] ON [PendingApprovals] ([ApproverId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_PendingApprovals_ModuleId] ON [PendingApprovals] ([ModuleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_SavingDepositLedgers_MemberId] ON [SavingDepositLedgers] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_SavingDepositTransactions_MemberId] ON [SavingDepositTransactions] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_SavingsConfigurations_MemberTypeId] ON [SavingsConfigurations] ([MemberTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    CREATE INDEX [IX_VoucherTransactions_TransactionTypeId] ON [VoucherTransactions] ([TransactionTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209122346_BusinessEntitiesUpdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210209122346_BusinessEntitiesUpdate', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    ALTER TABLE [ModuleApprovers] DROP CONSTRAINT [FK_ModuleApprovers_Approvers_ApproverId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    ALTER TABLE [PendingApprovals] DROP CONSTRAINT [FK_PendingApprovals_Approvers_ApproverId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    ALTER TABLE [PendingApprovals] DROP CONSTRAINT [FK_PendingApprovals_Modules_ModuleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    DROP INDEX [IX_PendingApprovals_ApproverId] ON [PendingApprovals];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    DROP INDEX [IX_ModuleApprovers_ApproverId] ON [ModuleApprovers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PendingApprovals]') AND [c].[name] = N'ApproverId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [PendingApprovals] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [PendingApprovals] DROP COLUMN [ApproverId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ModuleApprovers]') AND [c].[name] = N'ApproverId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ModuleApprovers] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [ModuleApprovers] DROP COLUMN [ApproverId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    EXEC sp_rename N'[PendingApprovals].[ModuleId]', N'ModuleApproverId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    EXEC sp_rename N'[PendingApprovals].[IX_PendingApprovals_ModuleId]', N'IX_PendingApprovals_ModuleApproverId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    ALTER TABLE [PendingApprovals] ADD [Approved] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    ALTER TABLE [Members] ADD [ApprovalCount] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    ALTER TABLE [Members] ADD [Approved] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    CREATE TABLE [ModuleApproverNameStore] (
        [Id] int NOT NULL IDENTITY,
        [ModuleApproverId] int NOT NULL,
        [Usernames] nvarchar(max) NULL,
        [ApprovalLevel] int NOT NULL,
        CONSTRAINT [PK_ModuleApproverNameStore] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ModuleApproverNameStore_ModuleApprovers_ModuleApproverId] FOREIGN KEY ([ModuleApproverId]) REFERENCES [ModuleApprovers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    CREATE INDEX [IX_ModuleApproverNameStore_ModuleApproverId] ON [ModuleApproverNameStore] ([ModuleApproverId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    ALTER TABLE [PendingApprovals] ADD CONSTRAINT [FK_PendingApprovals_ModuleApprovers_ModuleApproverId] FOREIGN KEY ([ModuleApproverId]) REFERENCES [ModuleApprovers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210190214_ApprovalEntities')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210210190214_ApprovalEntities', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210214110214_ModulesSavingsLedgerDeosit-Edit')
BEGIN
    EXEC sp_rename N'[SavingDepositLedgers].[TrancastionTypeId]', N'TransactionTypeId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210214110214_ModulesSavingsLedgerDeosit-Edit')
BEGIN
    ALTER TABLE [TransactionTypes] ADD [NormalizedName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210214110214_ModulesSavingsLedgerDeosit-Edit')
BEGIN
    ALTER TABLE [Modules] ADD [NormalizedName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210214110214_ModulesSavingsLedgerDeosit-Edit')
BEGIN
    CREATE INDEX [IX_SavingDepositTransactions_TransactionTypeId] ON [SavingDepositTransactions] ([TransactionTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210214110214_ModulesSavingsLedgerDeosit-Edit')
BEGIN
    CREATE INDEX [IX_SavingDepositLedgers_TransactionTypeId] ON [SavingDepositLedgers] ([TransactionTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210214110214_ModulesSavingsLedgerDeosit-Edit')
BEGIN
    ALTER TABLE [SavingDepositLedgers] ADD CONSTRAINT [FK_SavingDepositLedgers_TransactionTypes_TransactionTypeId] FOREIGN KEY ([TransactionTypeId]) REFERENCES [TransactionTypes] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210214110214_ModulesSavingsLedgerDeosit-Edit')
BEGIN
    ALTER TABLE [SavingDepositTransactions] ADD CONSTRAINT [FK_SavingDepositTransactions_TransactionTypes_TransactionTypeId] FOREIGN KEY ([TransactionTypeId]) REFERENCES [TransactionTypes] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210214110214_ModulesSavingsLedgerDeosit-Edit')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210214110214_ModulesSavingsLedgerDeosit-Edit', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210217160859_ApprovalEntityEdit')
BEGIN
    ALTER TABLE [PendingApprovals] ADD [CurrentLevel] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210217160859_ApprovalEntityEdit')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210217160859_ApprovalEntityEdit', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210218093116_IncludedTagInTrxAndMember')
BEGIN
    ALTER TABLE [SavingDepositTransactions] ADD [Tag] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210218093116_IncludedTagInTrxAndMember')
BEGIN
    ALTER TABLE [Members] ADD [Tag] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210218093116_IncludedTagInTrxAndMember')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210218093116_IncludedTagInTrxAndMember', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210220162720_concurrentloancount')
BEGIN
    ALTER TABLE [LoanConfigs] ADD [ConcurrentLoanCount] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210220162720_concurrentloancount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210220162720_concurrentloancount', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210220165413_loanconfig')
BEGIN
    ALTER TABLE [LoanConfigs] ADD [AllowTenureAdjustment] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210220165413_loanconfig')
BEGIN
    ALTER TABLE [LoanConfigs] ADD [AllowTopUp] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210220165413_loanconfig')
BEGIN
    ALTER TABLE [LoanConfigs] ADD [GuarantorCount] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210220165413_loanconfig')
BEGIN
    ALTER TABLE [LoanConfigs] ADD [RequiresGuarantors] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210220165413_loanconfig')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210220165413_loanconfig', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210220165722_guarantorscount')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanConfigs]') AND [c].[name] = N'GuarantorCount');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [LoanConfigs] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [LoanConfigs] ALTER COLUMN [GuarantorCount] int NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210220165722_guarantorscount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210220165722_guarantorscount', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210221132939_MemberBalances')
BEGIN
    CREATE TABLE [MemberBalances] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] int NOT NULL,
        [BalanceType] int NOT NULL,
        [CurrentBalance] decimal(10,2) NOT NULL,
        [ItemId] int NOT NULL,
        CONSTRAINT [PK_MemberBalances] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MemberBalances_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210221132939_MemberBalances')
BEGIN
    CREATE INDEX [IX_MemberBalances_MemberId] ON [MemberBalances] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210221132939_MemberBalances')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210221132939_MemberBalances', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210221213109_LoanInterest')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanRepayments]') AND [c].[name] = N'Intrest');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [LoanRepayments] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [LoanRepayments] DROP COLUMN [Intrest];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210221213109_LoanInterest')
BEGIN
    ALTER TABLE [LoanRepayments] ADD [Interest] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210221213109_LoanInterest')
BEGIN
    ALTER TABLE [LoanApplications] ADD [LoanCondition] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210221213109_LoanInterest')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210221213109_LoanInterest', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210222211958_InterestToDecimal')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanApplications]') AND [c].[name] = N'Intrest');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [LoanApplications] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [LoanApplications] ALTER COLUMN [Intrest] decimal(18,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210222211958_InterestToDecimal')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210222211958_InterestToDecimal', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210224182007_LastLoginAdded')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanApplications]') AND [c].[name] = N'Intrest');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [LoanApplications] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [LoanApplications] DROP COLUMN [Intrest];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210224182007_LastLoginAdded')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanRepayments]') AND [c].[name] = N'Interest');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [LoanRepayments] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [LoanRepayments] ALTER COLUMN [Interest] decimal(10,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210224182007_LastLoginAdded')
BEGIN
    ALTER TABLE [LoanApplications] ADD [Interest] decimal(10,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210224182007_LastLoginAdded')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [LastLogin] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210224182007_LastLoginAdded')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210224182007_LastLoginAdded', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210226164902_MemberAndPersonUpdate')
BEGIN
    ALTER TABLE [Persons] ADD [PersonalEmail] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210226164902_MemberAndPersonUpdate')
BEGIN
    ALTER TABLE [Members] ADD [EmploymentDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210226164902_MemberAndPersonUpdate')
BEGIN
    ALTER TABLE [Members] ADD [Location] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210226164902_MemberAndPersonUpdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210226164902_MemberAndPersonUpdate', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307093420_LoandGuarantors')
BEGIN
    ALTER TABLE [LoanConfigs] ADD [GuarantorMaximumAmount] decimal(10,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307093420_LoandGuarantors')
BEGIN
    ALTER TABLE [LoanConfigs] ADD [ServiceYearDuration] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307093420_LoandGuarantors')
BEGIN
    ALTER TABLE [LoanConfigs] ADD [UseYearsOfService] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307093420_LoandGuarantors')
BEGIN
    ALTER TABLE [LoanApplications] ADD [GuarantorApprovalCount] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307093420_LoandGuarantors')
BEGIN
    CREATE TABLE [LoanGuarantor] (
        [Id] int NOT NULL IDENTITY,
        [LoanId] int NOT NULL,
        [EmployeeNumber] nvarchar(max) NULL,
        [GurantorName] nvarchar(max) NULL,
        [GurantorEmail] nvarchar(max) NULL,
        [ApprovalDate] datetime2 NULL,
        [LoanApplicationId] int NULL,
        CONSTRAINT [PK_LoanGuarantor] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LoanGuarantor_LoanApplications_LoanApplicationId] FOREIGN KEY ([LoanApplicationId]) REFERENCES [LoanApplications] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_LoanGuarantor_Loans_LoanId] FOREIGN KEY ([LoanId]) REFERENCES [Loans] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307093420_LoandGuarantors')
BEGIN
    CREATE INDEX [IX_LoanGuarantor_LoanApplicationId] ON [LoanGuarantor] ([LoanApplicationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307093420_LoandGuarantors')
BEGIN
    CREATE INDEX [IX_LoanGuarantor_LoanId] ON [LoanGuarantor] ([LoanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307093420_LoandGuarantors')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210307093420_LoandGuarantors', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307094503_LoandGuarantorss')
BEGIN
    ALTER TABLE [LoanGuarantor] DROP CONSTRAINT [FK_LoanGuarantor_LoanApplications_LoanApplicationId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307094503_LoandGuarantorss')
BEGIN
    ALTER TABLE [LoanGuarantor] DROP CONSTRAINT [FK_LoanGuarantor_Loans_LoanId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307094503_LoandGuarantorss')
BEGIN
    ALTER TABLE [LoanGuarantor] DROP CONSTRAINT [PK_LoanGuarantor];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307094503_LoandGuarantorss')
BEGIN
    EXEC sp_rename N'[LoanGuarantor]', N'LoanGuarantors';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307094503_LoandGuarantorss')
BEGIN
    EXEC sp_rename N'[LoanGuarantors].[IX_LoanGuarantor_LoanId]', N'IX_LoanGuarantors_LoanId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307094503_LoandGuarantorss')
BEGIN
    EXEC sp_rename N'[LoanGuarantors].[IX_LoanGuarantor_LoanApplicationId]', N'IX_LoanGuarantors_LoanApplicationId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307094503_LoandGuarantorss')
BEGIN
    ALTER TABLE [LoanGuarantors] ADD CONSTRAINT [PK_LoanGuarantors] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307094503_LoandGuarantorss')
BEGIN
    ALTER TABLE [LoanGuarantors] ADD CONSTRAINT [FK_LoanGuarantors_LoanApplications_LoanApplicationId] FOREIGN KEY ([LoanApplicationId]) REFERENCES [LoanApplications] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307094503_LoandGuarantorss')
BEGIN
    ALTER TABLE [LoanGuarantors] ADD CONSTRAINT [FK_LoanGuarantors_Loans_LoanId] FOREIGN KEY ([LoanId]) REFERENCES [Loans] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307094503_LoandGuarantorss')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210307094503_LoandGuarantorss', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307113533_LoandGuarantorConfig')
BEGIN
    CREATE TABLE [LoanGuarantorConfigs] (
        [Id] int NOT NULL IDENTITY,
        [LoanId] int NOT NULL,
        [GuarantorMinimumAmount] decimal(18,2) NOT NULL,
        [GuarantorMaximumAmount] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_LoanGuarantorConfigs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LoanGuarantorConfigs_Loans_LoanId] FOREIGN KEY ([LoanId]) REFERENCES [Loans] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307113533_LoandGuarantorConfig')
BEGIN
    CREATE INDEX [IX_LoanGuarantorConfigs_LoanId] ON [LoanGuarantorConfigs] ([LoanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307113533_LoandGuarantorConfig')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210307113533_LoandGuarantorConfig', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307113902_LoandGuarantorConfigCount')
BEGIN
    ALTER TABLE [LoanGuarantorConfigs] ADD [GuarantorCount] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210307113902_LoandGuarantorConfigCount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210307113902_LoandGuarantorConfigCount', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308152658_LoandGuarantorRemoveLoanId')
BEGIN
    ALTER TABLE [LoanGuarantors] DROP CONSTRAINT [FK_LoanGuarantors_LoanApplications_LoanApplicationId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308152658_LoandGuarantorRemoveLoanId')
BEGIN
    ALTER TABLE [LoanGuarantors] DROP CONSTRAINT [FK_LoanGuarantors_Loans_LoanId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308152658_LoandGuarantorRemoveLoanId')
BEGIN
    DROP INDEX [IX_LoanGuarantors_LoanId] ON [LoanGuarantors];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308152658_LoandGuarantorRemoveLoanId')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanGuarantors]') AND [c].[name] = N'LoanId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [LoanGuarantors] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [LoanGuarantors] DROP COLUMN [LoanId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308152658_LoandGuarantorRemoveLoanId')
BEGIN
    DROP INDEX [IX_LoanGuarantors_LoanApplicationId] ON [LoanGuarantors];
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanGuarantors]') AND [c].[name] = N'LoanApplicationId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [LoanGuarantors] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [LoanGuarantors] ALTER COLUMN [LoanApplicationId] int NOT NULL;
    ALTER TABLE [LoanGuarantors] ADD DEFAULT 0 FOR [LoanApplicationId];
    CREATE INDEX [IX_LoanGuarantors_LoanApplicationId] ON [LoanGuarantors] ([LoanApplicationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308152658_LoandGuarantorRemoveLoanId')
BEGIN
    ALTER TABLE [LoanGuarantors] ADD CONSTRAINT [FK_LoanGuarantors_LoanApplications_LoanApplicationId] FOREIGN KEY ([LoanApplicationId]) REFERENCES [LoanApplications] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308152658_LoandGuarantorRemoveLoanId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210308152658_LoandGuarantorRemoveLoanId', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308154051_LoandGuarantorNameCorerction')
BEGIN
    EXEC sp_rename N'[LoanGuarantors].[GurantorName]', N'GuarantorName', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308154051_LoandGuarantorNameCorerction')
BEGIN
    EXEC sp_rename N'[LoanGuarantors].[GurantorEmail]', N'GuarantorEmail', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308154051_LoandGuarantorNameCorerction')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210308154051_LoandGuarantorNameCorerction', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308171414_RepaymentDateCorrection')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanRepayments]') AND [c].[name] = N'MemberId');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [LoanRepayments] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [LoanRepayments] DROP COLUMN [MemberId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308171414_RepaymentDateCorrection')
BEGIN
    EXEC sp_rename N'[LoanRepayments].[RepaymentDte]', N'RepaymentDate', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308171414_RepaymentDateCorrection')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210308171414_RepaymentDateCorrection', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210311121508_RequiredGuarantorsCount')
BEGIN
    ALTER TABLE [LoanApplications] ADD [RequiredGuarantorsCount] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210311121508_RequiredGuarantorsCount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210311121508_RequiredGuarantorsCount', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210311125710_GuarantorApprovalStatus')
BEGIN
    ALTER TABLE [LoanGuarantors] ADD [ApprovalStatus] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210311125710_GuarantorApprovalStatus')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210311125710_GuarantorApprovalStatus', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210311142432_GuarantorApprovalComment')
BEGIN
    ALTER TABLE [LoanGuarantors] ADD [Comments] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210311142432_GuarantorApprovalComment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210311142432_GuarantorApprovalComment', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316203404_LoanGuarantorConfigdd')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210316203404_LoanGuarantorConfigdd', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316203917_LoanGuarantorConfigddk')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210316203917_LoanGuarantorConfigddk', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316221851_UserTable-And-MemberUpdate')
BEGIN
    ALTER TABLE [Members] ADD [HasPaidFee] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316221851_UserTable-And-MemberUpdate')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [UserTypeCategory] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210316221851_UserTable-And-MemberUpdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210316221851_UserTable-And-MemberUpdate', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210331194835_LoanEntity-Update')
BEGIN
    ALTER TABLE [LoanApplications] ADD [Approved] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210331194835_LoanEntity-Update')
BEGIN
    ALTER TABLE [LoanApplications] ADD [IsPaid] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210331194835_LoanEntity-Update')
BEGIN
    ALTER TABLE [LoanApplications] ADD [Tag] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210331194835_LoanEntity-Update')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'AnnualSalary');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Employees] ALTER COLUMN [AnnualSalary] decimal(18,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210331194835_LoanEntity-Update')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210331194835_LoanEntity-Update', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402121254_RetirementDate')
BEGIN
    ALTER TABLE [Members] ADD [RetirementDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402121254_RetirementDate')
BEGIN
    CREATE TABLE [Retireeexport] (
        [EMPNO] nvarchar(450) NOT NULL,
        [PINNO] nvarchar(max) NULL,
        [EMPLOYEENO] nvarchar(max) NULL,
        [JOBTITLE] nvarchar(max) NULL,
        [DIVISIONNAME] nvarchar(max) NULL,
        [SURNAME] nvarchar(max) NULL,
        [FIRSTNAME] nvarchar(max) NULL,
        [MIDDLENAME] nvarchar(max) NULL,
        [EMAILADDRESS] nvarchar(max) NULL,
        [ALTEMAILADDRESS] nvarchar(max) NULL,
        [DIRECTPHONE] nvarchar(max) NULL,
        [ALTPHONE] nvarchar(max) NULL,
        [LOCATIONNAME] nvarchar(max) NULL,
        [RETIREMENTDATE] nvarchar(max) NULL,
        [EMPLOYMENTDATE] nvarchar(max) NULL,
        [NATIONALITY] nvarchar(max) NULL,
        CONSTRAINT [PK_Retireeexport] PRIMARY KEY ([EMPNO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402121254_RetirementDate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210402121254_RetirementDate', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    ALTER TABLE [LoanApplications] ADD [AccountName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    ALTER TABLE [LoanApplications] ADD [AccountNumber] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    ALTER TABLE [LoanApplications] ADD [BankName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE TABLE [TransferApplications] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] int NOT NULL,
        [SourceSavingsType] int NOT NULL,
        [DestinationSavingsType] int NOT NULL,
        [TransactionDate] datetime2 NOT NULL,
        [EffectiveDate] datetime2 NOT NULL,
        [Amount] decimal(10,2) NOT NULL,
        [MethodOfCollectionId] int NOT NULL,
        [ApprovalCount] int NOT NULL,
        [LastModifiedDate] datetime2 NULL,
        [LastApprovalDate] datetime2 NULL,
        [CreatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_TransferApplications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TransferApplications_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE TABLE [WaiverApplication] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] int NOT NULL,
        [ApplicatiinDate] datetime2 NOT NULL,
        [TotalWaiverFee] decimal(18,2) NOT NULL,
        [Commnents] nvarchar(max) NULL,
        [ReceiptNo] nvarchar(max) NULL,
        [ApprovalCount] int NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [LastApprovalDate] datetime2 NULL,
        [ModeOfPaymentId] int NULL,
        CONSTRAINT [PK_WaiverApplication] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_WaiverApplication_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_WaiverApplication_ModeOfPayments_ModeOfPaymentId] FOREIGN KEY ([ModeOfPaymentId]) REFERENCES [ModeOfPayments] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE TABLE [WaiverTypes] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_WaiverTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE TABLE [WithdrawalApplications] (
        [Id] int NOT NULL IDENTITY,
        [MemberId] int NOT NULL,
        [SourceSavingsType] int NOT NULL,
        [TransactionDate] datetime2 NOT NULL,
        [EffectiveDate] datetime2 NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [MethodOfCollectionId] int NOT NULL,
        [ApprovalCount] int NOT NULL,
        [CollectionBankId] int NOT NULL,
        [AccountName] nvarchar(max) NULL,
        [AccountNumber] nvarchar(max) NULL,
        [CollectionLocation] nvarchar(max) NULL,
        [LastModifiedDate] datetime2 NULL,
        [LastApprovalDate] datetime2 NULL,
        [CreatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_WithdrawalApplications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_WithdrawalApplications_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_WithdrawalApplications_MethodOfCollections_MethodOfCollectionId] FOREIGN KEY ([MethodOfCollectionId]) REFERENCES [MethodOfCollections] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE TABLE [WaiverApplicationDetails] (
        [Id] int NOT NULL IDENTITY,
        [WaiverApplicationId] int NOT NULL,
        [WaiverTypeId] int NOT NULL,
        [LoanAmount] decimal(18,2) NOT NULL,
        [Interest] decimal(18,2) NOT NULL,
        [Fee] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_WaiverApplicationDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_WaiverApplicationDetails_WaiverApplication_WaiverApplicationId] FOREIGN KEY ([WaiverApplicationId]) REFERENCES [WaiverApplication] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_WaiverApplicationDetails_WaiverTypes_WaiverTypeId] FOREIGN KEY ([WaiverTypeId]) REFERENCES [WaiverTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE INDEX [IX_TransferApplications_MemberId] ON [TransferApplications] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE INDEX [IX_WaiverApplication_MemberId] ON [WaiverApplication] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE INDEX [IX_WaiverApplication_ModeOfPaymentId] ON [WaiverApplication] ([ModeOfPaymentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE INDEX [IX_WaiverApplicationDetails_WaiverApplicationId] ON [WaiverApplicationDetails] ([WaiverApplicationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE INDEX [IX_WaiverApplicationDetails_WaiverTypeId] ON [WaiverApplicationDetails] ([WaiverTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE INDEX [IX_WithdrawalApplications_MemberId] ON [WithdrawalApplications] ([MemberId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    CREATE INDEX [IX_WithdrawalApplications_MethodOfCollectionId] ON [WithdrawalApplications] ([MethodOfCollectionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405154125_LoanBankDetails')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210405154125_LoanBankDetails', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405155028_LoanBankDetailCode')
BEGIN
    ALTER TABLE [LoanApplications] ADD [BankCode] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210405155028_LoanBankDetailCode')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210405155028_LoanBankDetailCode', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408133223_WithdrawalBank')
BEGIN
    EXEC sp_rename N'[WithdrawalApplications].[CollectionBankId]', N'BankId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408133223_WithdrawalBank')
BEGIN
    CREATE INDEX [IX_WithdrawalApplications_BankId] ON [WithdrawalApplications] ([BankId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408133223_WithdrawalBank')
BEGIN
    ALTER TABLE [WithdrawalApplications] ADD CONSTRAINT [FK_WithdrawalApplications_Banks_BankId] FOREIGN KEY ([BankId]) REFERENCES [Banks] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408133223_WithdrawalBank')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210408133223_WithdrawalBank', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414153348_TransferModeOfCollectionRemoval')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TransferApplications]') AND [c].[name] = N'MethodOfCollectionId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [TransferApplications] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [TransferApplications] DROP COLUMN [MethodOfCollectionId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414153348_TransferModeOfCollectionRemoval')
BEGIN
    ALTER TABLE [LoanApplications] ADD [Approved] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414153348_TransferModeOfCollectionRemoval')
BEGIN
    ALTER TABLE [LoanApplications] ADD [BankCode] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414153348_TransferModeOfCollectionRemoval')
BEGIN
    ALTER TABLE [LoanApplications] ADD [BankName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210414153348_TransferModeOfCollectionRemoval')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210414153348_TransferModeOfCollectionRemoval', N'5.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416021713_DecimalChange')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanConfigs]') AND [c].[name] = N'MonthlySavingsAmount');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [LoanConfigs] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [LoanConfigs] ALTER COLUMN [MonthlySavingsAmount] decimal(18,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416021713_DecimalChange')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanConfigs]') AND [c].[name] = N'MinLoanAmount');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [LoanConfigs] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [LoanConfigs] ALTER COLUMN [MinLoanAmount] decimal(18,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416021713_DecimalChange')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanConfigs]') AND [c].[name] = N'MaxLoanAmount');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [LoanConfigs] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [LoanConfigs] ALTER COLUMN [MaxLoanAmount] decimal(18,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416021713_DecimalChange')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanConfigs]') AND [c].[name] = N'LumpSumSavingsAmount');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [LoanConfigs] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [LoanConfigs] ALTER COLUMN [LumpSumSavingsAmount] decimal(18,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416021713_DecimalChange')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanConfigs]') AND [c].[name] = N'ExistingLoanFeeAmount');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [LoanConfigs] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [LoanConfigs] ALTER COLUMN [ExistingLoanFeeAmount] decimal(18,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416021713_DecimalChange')
BEGIN
    ALTER TABLE [Employees] ADD [UserId] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416021713_DecimalChange')
BEGIN
    CREATE TABLE [CreditCommitteeBalances] (
        [EMPNO] nvarchar(450) NOT NULL,
        [EmpName] nvarchar(max) NULL,
        [Savgs] nvarchar(max) NULL,
        [SDEP] nvarchar(max) NULL,
        [STLoan] nvarchar(max) NULL,
        [LTLoan] nvarchar(max) NULL,
        [HAPL] nvarchar(max) NULL,
        [Vehicle] nvarchar(max) NULL,
        [TSL1] nvarchar(max) NULL,
        [TSL2] nvarchar(max) NULL,
        [TSL3] nvarchar(max) NULL,
        [Executive] nvarchar(max) NULL,
        CONSTRAINT [PK_CreditCommitteeBalances] PRIMARY KEY ([EMPNO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416021713_DecimalChange')
BEGIN
    CREATE TABLE [RetireeBalances] (
        [EMPNO] nvarchar(450) NOT NULL,
        [NAME] nvarchar(max) NULL,
        [SAVINGS] nvarchar(max) NULL,
        [SPECDEP] nvarchar(max) NULL,
        [SHORTTERM] nvarchar(max) NULL,
        [LONGTERM] nvarchar(max) NULL,
        CONSTRAINT [PK_RetireeBalances] PRIMARY KEY ([EMPNO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416021713_DecimalChange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210416021713_DecimalChange', N'5.0.2');
END;
GO

COMMIT;
GO

