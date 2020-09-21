package main;

import com.opencsv.bean.CsvBindByName;

public class DataRaw {

    @CsvBindByName
    private int id;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    @CsvBindByName
    private String question;

    public String getQuestion() {
        return question;
    }

    public void setQuestion(String question) {
        this.question = question;
    }

    @CsvBindByName
    private String answer;

    public String getAnswer() {
        return answer;
    }

    public void setAnswer(String answer) {
        this.answer = answer;
    }

    @CsvBindByName
    private int tournament;

    public int getTournament() {
        return tournament;
    }

    public void setTournament(int tournament) {
        this.tournament = tournament;
    }

    @CsvBindByName
    private int category;

    public int getCategory() {
        return category;
    }

    public void setCategory(int category) {
        this.category = category;
    }

    @CsvBindByName
    private int subcategory;

    public int getSubcategory() {
        return subcategory;
    }

    public void setSubcategory(int subcategory) {
        this.subcategory = subcategory;
    }
}
