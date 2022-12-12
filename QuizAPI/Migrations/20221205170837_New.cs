using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_question_quiz_quiz_id",
                table: "question");

            migrationBuilder.DropForeignKey(
                name: "fk_quiz_users_author_id",
                table: "quiz");

            migrationBuilder.DropForeignKey(
                name: "fk_option_question_question_id",
                table: "option");

            migrationBuilder.DropForeignKey(
                name: "fk_take_test_test_id",
                table: "take");

            migrationBuilder.DropForeignKey(
                name: "fk_take_users_taker_id",
                table: "take");

            migrationBuilder.DropForeignKey(
                name: "fk_take_answer_option_option_id",
                table: "take_answer");

            migrationBuilder.DropForeignKey(
                name: "fk_take_answer_take_question_take_question_id",
                table: "take_answer");

            migrationBuilder.DropForeignKey(
                name: "fk_take_question_question_question_id",
                table: "take_question");

            migrationBuilder.DropForeignKey(
                name: "fk_take_question_take_take_id",
                table: "take_question");

            migrationBuilder.DropForeignKey(
                name: "fk_test_quiz_quiz_id",
                table: "test");

            migrationBuilder.DropPrimaryKey(
                name: "pk_test",
                table: "test");

            migrationBuilder.DropPrimaryKey(
                name: "pk_take_answer",
                table: "take_answer");

            migrationBuilder.DropPrimaryKey(
                name: "pk_take",
                table: "take");

            migrationBuilder.DropPrimaryKey(
                name: "pk_option",
                table: "option");

            migrationBuilder.DropPrimaryKey(
                name: "pk_quiz",
                table: "quiz");

            migrationBuilder.DropPrimaryKey(
                name: "pk_question",
                table: "question");

            migrationBuilder.RenameTable(
                name: "test",
                newName: "tests");

            migrationBuilder.RenameTable(
                name: "take_answer",
                newName: "take_answers");

            migrationBuilder.RenameTable(
                name: "take",
                newName: "takes");

            migrationBuilder.RenameTable(
                name: "option",
                newName: "options");

            migrationBuilder.RenameTable(
                name: "quiz",
                newName: "quizzes");

            migrationBuilder.RenameTable(
                name: "question",
                newName: "questions");

            migrationBuilder.RenameIndex(
                name: "ix_test_quiz_id",
                table: "tests",
                newName: "ix_tests_quiz_id");

            migrationBuilder.RenameIndex(
                name: "ix_take_answer_take_question_id",
                table: "take_answers",
                newName: "ix_take_answers_take_question_id");

            migrationBuilder.RenameIndex(
                name: "ix_take_answer_option_id",
                table: "take_answers",
                newName: "ix_take_answers_option_id");

            migrationBuilder.RenameIndex(
                name: "ix_take_test_id",
                table: "takes",
                newName: "ix_takes_test_id");

            migrationBuilder.RenameIndex(
                name: "ix_take_taker_id",
                table: "takes",
                newName: "ix_takes_taker_id");

            migrationBuilder.RenameIndex(
                name: "ix_option_question_id",
                table: "options",
                newName: "ix_options_question_id");

            migrationBuilder.RenameIndex(
                name: "ix_quiz_author_id",
                table: "quizzes",
                newName: "ix_quizzes_author_id");

            migrationBuilder.RenameIndex(
                name: "ix_question_quiz_id",
                table: "questions",
                newName: "ix_questions_quiz_id");

            migrationBuilder.AlterColumn<string>(
                name: "taker_id",
                table: "takes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "author_id",
                table: "quizzes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tests",
                table: "tests",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_take_answers",
                table: "take_answers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_takes",
                table: "takes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_options",
                table: "options",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_quizzes",
                table: "quizzes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_questions",
                table: "questions",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_questions_quizzes_quiz_id",
                table: "questions",
                column: "quiz_id",
                principalTable: "quizzes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_quizzes_users_author_id",
                table: "quizzes",
                column: "author_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_options_questions_question_id",
                table: "options",
                column: "question_id",
                principalTable: "questions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_take_answers_options_option_id",
                table: "take_answers",
                column: "option_id",
                principalTable: "options",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_take_answers_take_question_take_question_id",
                table: "take_answers",
                column: "take_question_id",
                principalTable: "take_question",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_take_question_questions_question_id",
                table: "take_question",
                column: "question_id",
                principalTable: "questions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_take_question_takes_take_id",
                table: "take_question",
                column: "take_id",
                principalTable: "takes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_takes_tests_test_id",
                table: "takes",
                column: "test_id",
                principalTable: "tests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_takes_users_taker_id",
                table: "takes",
                column: "taker_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_tests_quizzes_quiz_id",
                table: "tests",
                column: "quiz_id",
                principalTable: "quizzes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_questions_quizzes_quiz_id",
                table: "questions");

            migrationBuilder.DropForeignKey(
                name: "fk_quizzes_users_author_id",
                table: "quizzes");

            migrationBuilder.DropForeignKey(
                name: "fk_options_questions_question_id",
                table: "options");

            migrationBuilder.DropForeignKey(
                name: "fk_take_answers_options_option_id",
                table: "take_answers");

            migrationBuilder.DropForeignKey(
                name: "fk_take_answers_take_question_take_question_id",
                table: "take_answers");

            migrationBuilder.DropForeignKey(
                name: "fk_take_question_questions_question_id",
                table: "take_question");

            migrationBuilder.DropForeignKey(
                name: "fk_take_question_takes_take_id",
                table: "take_question");

            migrationBuilder.DropForeignKey(
                name: "fk_takes_tests_test_id",
                table: "takes");

            migrationBuilder.DropForeignKey(
                name: "fk_takes_users_taker_id",
                table: "takes");

            migrationBuilder.DropForeignKey(
                name: "fk_tests_quizzes_quiz_id",
                table: "tests");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tests",
                table: "tests");

            migrationBuilder.DropPrimaryKey(
                name: "pk_takes",
                table: "takes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_take_answers",
                table: "take_answers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_options",
                table: "options");

            migrationBuilder.DropPrimaryKey(
                name: "pk_quizzes",
                table: "quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_questions",
                table: "questions");

            migrationBuilder.RenameTable(
                name: "tests",
                newName: "test");

            migrationBuilder.RenameTable(
                name: "takes",
                newName: "take");

            migrationBuilder.RenameTable(
                name: "take_answers",
                newName: "take_answer");

            migrationBuilder.RenameTable(
                name: "options",
                newName: "option");

            migrationBuilder.RenameTable(
                name: "quizzes",
                newName: "quiz");

            migrationBuilder.RenameTable(
                name: "questions",
                newName: "question");

            migrationBuilder.RenameIndex(
                name: "ix_tests_quiz_id",
                table: "test",
                newName: "ix_test_quiz_id");

            migrationBuilder.RenameIndex(
                name: "ix_takes_test_id",
                table: "take",
                newName: "ix_take_test_id");

            migrationBuilder.RenameIndex(
                name: "ix_takes_taker_id",
                table: "take",
                newName: "ix_take_taker_id");

            migrationBuilder.RenameIndex(
                name: "ix_take_answers_take_question_id",
                table: "take_answer",
                newName: "ix_take_answer_take_question_id");

            migrationBuilder.RenameIndex(
                name: "ix_take_answers_option_id",
                table: "take_answer",
                newName: "ix_take_answer_option_id");

            migrationBuilder.RenameIndex(
                name: "ix_options_question_id",
                table: "option",
                newName: "ix_option_question_id");

            migrationBuilder.RenameIndex(
                name: "ix_quizzes_author_id",
                table: "quiz",
                newName: "ix_quiz_author_id");

            migrationBuilder.RenameIndex(
                name: "ix_questions_quiz_id",
                table: "question",
                newName: "ix_question_quiz_id");

            migrationBuilder.AlterColumn<string>(
                name: "taker_id",
                table: "take",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "author_id",
                table: "quiz",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_test",
                table: "test",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_take",
                table: "take",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_take_answer",
                table: "take_answer",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_option",
                table: "option",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_quiz",
                table: "quiz",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_question",
                table: "question",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_question_quiz_quiz_id",
                table: "question",
                column: "quiz_id",
                principalTable: "quiz",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_quiz_users_author_id",
                table: "quiz",
                column: "author_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_option_question_question_id",
                table: "option",
                column: "question_id",
                principalTable: "question",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_take_test_test_id",
                table: "take",
                column: "test_id",
                principalTable: "test",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_take_users_taker_id",
                table: "take",
                column: "taker_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_take_answer_option_option_id",
                table: "take_answer",
                column: "option_id",
                principalTable: "option",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_take_answer_take_question_take_question_id",
                table: "take_answer",
                column: "take_question_id",
                principalTable: "take_question",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_take_question_question_question_id",
                table: "take_question",
                column: "question_id",
                principalTable: "question",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_take_question_take_take_id",
                table: "take_question",
                column: "take_id",
                principalTable: "take",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_test_quiz_quiz_id",
                table: "test",
                column: "quiz_id",
                principalTable: "quiz",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
