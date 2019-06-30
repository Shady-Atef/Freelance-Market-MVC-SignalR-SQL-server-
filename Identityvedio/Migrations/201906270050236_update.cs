namespace Identityvedio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        SSN = c.String(nullable: false),
                        Profesion = c.String(),
                        Description = c.String(nullable: false),
                        ImagePath = c.String(),
                        ClientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false),
                        CatID = c.Int(nullable: false),
                        Desc = c.String(nullable: false),
                        ClientId = c.String(maxLength: 128),
                        Price = c.Int(),
                        PostTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        Ended = c.Boolean(nullable: false),
                        ExperienceLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId)
                .ForeignKey("dbo.JobCategories", t => t.CatID)
                .ForeignKey("dbo.JobExperienceLevels", t => t.ExperienceLevelId)
                .Index(t => t.CatID)
                .Index(t => t.ClientId)
                .Index(t => t.ExperienceLevelId);
            
            CreateTable(
                "dbo.JobCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SkillsName = c.String(),
                        CatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JobCategories", t => t.CatId)
                .Index(t => t.CatId);
            
            CreateTable(
                "dbo.JobSkills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Jobs", t => t.JobId)
                .ForeignKey("dbo.Skills", t => t.SkillId)
                .Index(t => t.JobId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.JobExperienceLevels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExperienceLevel = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProposalId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        FreelanceId = c.String(maxLength: 128),
                        ClientId = c.String(maxLength: 128),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        FinalPrice = c.Int(),
                        Hours = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ClientId)
                .ForeignKey("dbo.AspNetUsers", t => t.FreelanceId)
                .ForeignKey("dbo.Jobs", t => t.JobId)
                .ForeignKey("dbo.Proposals", t => t.ProposalId)
                .Index(t => t.ProposalId)
                .Index(t => t.JobId)
                .Index(t => t.FreelanceId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Proposals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        proposal = c.String(),
                        proposaldate = c.DateTime(nullable: false),
                        Duration = c.String(),
                        FilePath = c.String(),
                        JobId = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        FreelancerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.FreelancerId)
                .ForeignKey("dbo.Jobs", t => t.JobId)
                .Index(t => t.JobId)
                .Index(t => t.FreelancerId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FreelanceId = c.String(maxLength: 128),
                        ManagerId = c.String(maxLength: 128),
                        MessageText = c.String(),
                        MessageTime = c.DateTime(nullable: false),
                        ProposalId = c.Int(nullable: false),
                        Sender = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.FreelanceId)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerId)
                .ForeignKey("dbo.Proposals", t => t.ProposalId)
                .Index(t => t.FreelanceId)
                .Index(t => t.ManagerId)
                .Index(t => t.ProposalId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Messages", "ProposalId", "dbo.Proposals");
            DropForeignKey("dbo.Messages", "ManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "FreelanceId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "ProposalId", "dbo.Proposals");
            DropForeignKey("dbo.Proposals", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Proposals", "FreelancerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Contracts", "FreelanceId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contracts", "ClientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Clients", "ClientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Jobs", "ExperienceLevelId", "dbo.JobExperienceLevels");
            DropForeignKey("dbo.Jobs", "CatID", "dbo.JobCategories");
            DropForeignKey("dbo.JobSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.JobSkills", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Skills", "CatId", "dbo.JobCategories");
            DropForeignKey("dbo.Jobs", "ClientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Messages", new[] { "ProposalId" });
            DropIndex("dbo.Messages", new[] { "ManagerId" });
            DropIndex("dbo.Messages", new[] { "FreelanceId" });
            DropIndex("dbo.Proposals", new[] { "FreelancerId" });
            DropIndex("dbo.Proposals", new[] { "JobId" });
            DropIndex("dbo.Contracts", new[] { "ClientId" });
            DropIndex("dbo.Contracts", new[] { "FreelanceId" });
            DropIndex("dbo.Contracts", new[] { "JobId" });
            DropIndex("dbo.Contracts", new[] { "ProposalId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.JobSkills", new[] { "SkillId" });
            DropIndex("dbo.JobSkills", new[] { "JobId" });
            DropIndex("dbo.Skills", new[] { "CatId" });
            DropIndex("dbo.Jobs", new[] { "ExperienceLevelId" });
            DropIndex("dbo.Jobs", new[] { "ClientId" });
            DropIndex("dbo.Jobs", new[] { "CatID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Clients", new[] { "ClientId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.Proposals");
            DropTable("dbo.Contracts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.JobExperienceLevels");
            DropTable("dbo.JobSkills");
            DropTable("dbo.Skills");
            DropTable("dbo.JobCategories");
            DropTable("dbo.Jobs");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Clients");
        }
    }
}
