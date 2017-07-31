﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebService.Models.DatabaseModels;

namespace WebService.Migrations
{
    [DbContext(typeof(ZivaContext))]
    [Migration("20170728020758_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebService.Models.DatabaseModels.UserStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Gender");

                    b.Property<double>("Height");

                    b.Property<string>("Name");

                    b.Property<int>("ProfileType");

                    b.Property<int>("UnitType");

                    b.Property<string>("UserId");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.ToTable("UserStats");
                });
        }
    }
}
