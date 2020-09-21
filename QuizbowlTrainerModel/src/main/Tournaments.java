package main;

import com.opencsv.bean.CsvBindByName;

public class Tournaments {

    @CsvBindByName
    private int id;

    @CsvBindByName
    private int year;

    @CsvBindByName
    private String name;

    @CsvBindByName
    private int difficulty;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getYear() {
        return year;
    }

    public void setYear(int year) {
        this.year = year;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getDifficulty() {
        return difficulty;
    }

    public void setDifficulty(int difficulty) {
        this.difficulty = difficulty;
    }
}
