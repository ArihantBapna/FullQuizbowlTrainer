package main;

import java.io.Serializable;
import java.util.List;

public class AnswerModel implements Serializable{
    public String getAnswer() {
        return Answer;
    }

    public void setAnswer(String answer) {
        Answer = answer;
    }

    public List<QuestionModel> getQuestions() {
        return Questions;
    }

    public void setQuestions(List<QuestionModel> questions) {
        Questions = questions;
    }

    private String Answer;
    private int Size;

    public int getSize() {
        return Size;
    }

    public void setSize(int size) {
        Size = size;
    }

    private List<QuestionModel> Questions;

}
