﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using QuizAPI;

#nullable disable

namespace QuizAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("QuizAPI.Authentication.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_users");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("QuizAPI.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer")
                        .HasColumnName("difficulty");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("question_text");

                    b.Property<Guid>("QuizId")
                        .HasColumnType("uuid")
                        .HasColumnName("quiz_id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_questions");

                    b.HasIndex("QuizId")
                        .HasDatabaseName("ix_questions_quiz_id");

                    b.ToTable("questions", (string)null);
                });

            modelBuilder.Entity("QuizAPI.Models.Quiz", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text")
                        .HasColumnName("author_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_quizzes");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_quizzes_author_id");

                    b.ToTable("quizzes", (string)null);
                });

            modelBuilder.Entity("QuizAPI.Models.Option", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean")
                        .HasColumnName("is_correct");

                    b.Property<string>("OptionText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("option_text");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid")
                        .HasColumnName("question_id");

                    b.HasKey("Id")
                        .HasName("pk_options");

                    b.HasIndex("QuestionId")
                        .HasDatabaseName("ix_options_question_id");

                    b.ToTable("options", (string)null);
                });

            modelBuilder.Entity("QuizAPI.Models.Take", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AnonymousCheckIdentifier")
                        .HasColumnType("uuid")
                        .HasColumnName("anonymous_check_identifier");

                    b.Property<double>("Score")
                        .HasColumnType("double precision")
                        .HasColumnName("score");

                    b.Property<double>("SuccessRate")
                        .HasColumnType("double precision")
                        .HasColumnName("success_rate");

                    b.Property<string>("TakerId")
                        .HasColumnType("text")
                        .HasColumnName("taker_id");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uuid")
                        .HasColumnName("test_id");

                    b.HasKey("Id")
                        .HasName("pk_takes");

                    b.HasIndex("TakerId")
                        .HasDatabaseName("ix_takes_taker_id");

                    b.HasIndex("TestId")
                        .HasDatabaseName("ix_takes_test_id");

                    b.ToTable("takes", (string)null);
                });

            modelBuilder.Entity("QuizAPI.Models.TakeAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("OptionId")
                        .HasColumnType("uuid")
                        .HasColumnName("option_id");

                    b.Property<Guid>("TakeQuestionId")
                        .HasColumnType("uuid")
                        .HasColumnName("take_question_id");

                    b.HasKey("Id")
                        .HasName("pk_take_answers");

                    b.HasIndex("OptionId")
                        .HasDatabaseName("ix_take_answers_option_id");

                    b.HasIndex("TakeQuestionId")
                        .HasDatabaseName("ix_take_answers_take_question_id");

                    b.ToTable("take_answers", (string)null);
                });

            modelBuilder.Entity("QuizAPI.Models.TakeQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("OpenEndedAnswer")
                        .HasColumnType("text")
                        .HasColumnName("open_ended_answer");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid")
                        .HasColumnName("question_id");

                    b.Property<double>("Score")
                        .HasColumnType("double precision")
                        .HasColumnName("score");

                    b.Property<Guid>("TakeId")
                        .HasColumnType("uuid")
                        .HasColumnName("take_id");

                    b.HasKey("Id")
                        .HasName("pk_take_question");

                    b.HasIndex("QuestionId")
                        .HasDatabaseName("ix_take_question_question_id");

                    b.HasIndex("TakeId")
                        .HasDatabaseName("ix_take_question_take_id");

                    b.ToTable("take_question", (string)null);
                });

            modelBuilder.Entity("QuizAPI.Models.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("EasyQuestionCount")
                        .HasColumnType("integer")
                        .HasColumnName("easy_question_count");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<int>("HardQuestionCount")
                        .HasColumnType("integer")
                        .HasColumnName("hard_question_count");

                    b.Property<int>("MediumQuestionCount")
                        .HasColumnType("integer")
                        .HasColumnName("medium_question_count");

                    b.Property<Guid>("QuizId")
                        .HasColumnType("uuid")
                        .HasColumnName("quiz_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_tests");

                    b.HasIndex("QuizId")
                        .HasDatabaseName("ix_tests_quiz_id");

                    b.ToTable("tests", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_role_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_role_claims_role_id");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_user_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_claims_user_id");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_asp_net_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_logins_user_id");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_asp_net_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_user_roles_role_id");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_asp_net_user_tokens");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("QuizAPI.Models.Question", b =>
                {
                    b.HasOne("QuizAPI.Models.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_questions_quizzes_quiz_id");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("QuizAPI.Models.Quiz", b =>
                {
                    b.HasOne("QuizAPI.Authentication.ApplicationUser", "Author")
                        .WithMany("Quizzes")
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("fk_quizzes_users_author_id");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("QuizAPI.Models.Option", b =>
                {
                    b.HasOne("QuizAPI.Models.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_options_questions_question_id");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuizAPI.Models.Take", b =>
                {
                    b.HasOne("QuizAPI.Authentication.ApplicationUser", "Taker")
                        .WithMany("Takes")
                        .HasForeignKey("TakerId")
                        .HasConstraintName("fk_takes_users_taker_id");

                    b.HasOne("QuizAPI.Models.Test", "Test")
                        .WithMany("Takes")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_takes_tests_test_id");

                    b.Navigation("Taker");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("QuizAPI.Models.TakeAnswer", b =>
                {
                    b.HasOne("QuizAPI.Models.Option", "Option")
                        .WithMany("TakeAnswers")
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_take_answers_options_option_id");

                    b.HasOne("QuizAPI.Models.TakeQuestion", "TakeQuestion")
                        .WithMany("TakeAnswers")
                        .HasForeignKey("TakeQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_take_answers_take_question_take_question_id");

                    b.Navigation("Option");

                    b.Navigation("TakeQuestion");
                });

            modelBuilder.Entity("QuizAPI.Models.TakeQuestion", b =>
                {
                    b.HasOne("QuizAPI.Models.Question", "Question")
                        .WithMany("TakeQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_take_question_questions_question_id");

                    b.HasOne("QuizAPI.Models.Take", "Take")
                        .WithMany("TakeQuestions")
                        .HasForeignKey("TakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_take_question_takes_take_id");

                    b.Navigation("Question");

                    b.Navigation("Take");
                });

            modelBuilder.Entity("QuizAPI.Models.Test", b =>
                {
                    b.HasOne("QuizAPI.Models.Quiz", "Quiz")
                        .WithMany("Tests")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tests_quizzes_quiz_id");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_role_claims_asp_net_roles_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("QuizAPI.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_claims_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("QuizAPI.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_logins_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_roles_role_id");

                    b.HasOne("QuizAPI.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("QuizAPI.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_tokens_asp_net_users_user_id");
                });

            modelBuilder.Entity("QuizAPI.Authentication.ApplicationUser", b =>
                {
                    b.Navigation("Quizzes");

                    b.Navigation("Takes");
                });

            modelBuilder.Entity("QuizAPI.Models.Question", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("TakeQuestions");
                });

            modelBuilder.Entity("QuizAPI.Models.Quiz", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Tests");
                });

            modelBuilder.Entity("QuizAPI.Models.Option", b =>
                {
                    b.Navigation("TakeAnswers");
                });

            modelBuilder.Entity("QuizAPI.Models.Take", b =>
                {
                    b.Navigation("TakeQuestions");
                });

            modelBuilder.Entity("QuizAPI.Models.TakeQuestion", b =>
                {
                    b.Navigation("TakeAnswers");
                });

            modelBuilder.Entity("QuizAPI.Models.Test", b =>
                {
                    b.Navigation("Takes");
                });
#pragma warning restore 612, 618
        }
    }
}
