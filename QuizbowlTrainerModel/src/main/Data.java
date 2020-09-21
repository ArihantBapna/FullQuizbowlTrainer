package main;

import com.opencsv.bean.CsvBindByName;
import com.opencsv.bean.CsvBindByPosition;

public class Data {

    @CsvBindByName(column = "Id")
    //@CsvBindByPosition(position = 0)
    private int id;

    @CsvBindByName(column = "Question")
    //@CsvBindByPosition(position = 1)
    private String question;

    @CsvBindByName(column = "Answer")
    //@CsvBindByPosition(position = 2)
    private String answer;

    @CsvBindByName(column = "Difficulty")
    //@CsvBindByPosition(position = 3)
    private int difficulty;

    @CsvBindByName(column = "TournamentId")
    //@CsvBindByPosition(position = 4)
    private int tournamentId;


    @CsvBindByName(column = "Tournament")
    //@CsvBindByPosition(position = 5)
    private String Tournament;

    @CsvBindByName(column = "Category")
    //@CsvBindByPosition(position = 6)
    private int category;

    @CsvBindByName(column = "Subcategory")
    //@CsvBindByPosition(position = 7)
    private int subcategory;

    @CsvBindByName(column = "Prompt")
    //@CsvBindByPosition(position = 8)
    private String prompt;

    @CsvBindByName(column = "Alternate")
    //@CsvBindByPosition(position = 9)
    private String alternate;

    @CsvBindByName(column = "AnswerId")
    private int ansId = 0;

    @CsvBindByName(column = "Answered")
    private int answered = 0;

    public int getAnswered() {
        return answered;
    }

    public void setAnswered(int answered) {
        this.answered = answered;
    }


    public int getAnsId() {
        return ansId;
    }

    public void setAnsId(int ansId) {
        this.ansId = ansId;
    }



    public String getTournament() {
        return Tournament;
    }

    public void setTournament(String tournament) {
        Tournament = tournament;
    }

    public String getAlternate() {
        return alternate;
    }

    public void setAlternate(String alternate) {
        this.alternate = alternate;
    }



    public int getDifficulty() {
        return difficulty;
    }

    public void setDifficulty(int difficulty) {
        this.difficulty = difficulty;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getQuestion() {
        return question;
    }

    public void setQuestion(String question) {
        this.question = question;
    }

    public String getAnswer() {
        return answer;
    }

    public void setAnswer(String answer) {
        this.answer = answer;
    }

    public int getTournamentId() {
        return tournamentId;
    }

    public void setTournamentId(int tournamentId) {
        this.tournamentId = tournamentId;
    }

    public int getCategory() {
        return category;
    }

    public void setCategory(int category) {
        this.category = category;
    }

    public int getSubcategory() {
        return subcategory;
    }

    public void setSubcategory(int subcategory) {
        this.subcategory = subcategory;
    }

    public String getPrompt() {
        return prompt;
    }

    public void setPrompt(String prompt) {
        this.prompt = prompt;
    }


}
