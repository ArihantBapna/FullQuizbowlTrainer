package main;

import java.io.*;
import java.util.ArrayList;
import java.util.List;

public class AnswerWeighting {

    public static void SetAnswerWeights(List<Data> qb) throws IOException {
        List<String> answersDone = new ArrayList<>();
        List<AnswerModel> Answers = new ArrayList<>();
        int i = 1;
        //Going through every entry in the database
        for(Data d : qb){
            if(i % 1000 == 0){
                System.out.println("On " +i);
            }
            i++;
            //Pull out the answer from the question entry
            String a1 = d.getAnswer().trim();
            int id1 = d.getId();
            //If we haven't already traversed for this answer
            if(!answersDone.contains(a1)){
                //Add the answer to the traversed list
                answersDone.add(a1);

                //Create an answer model for it
                AnswerModel ans = new AnswerModel();
                List<QuestionModel> ques = new ArrayList<>();

                //Loop through the entire database again
                for(Data d2 : qb){
                    String a2 = d2.getAnswer().trim();
                    int id2 = d2.getId();
                    //See that they are two different questions
                    if(id2 != id1){
                        //If the answers match
                        if(a2.equalsIgnoreCase(a1)){
                            //Create a question model and add it to the questions arraylist
                            QuestionModel q = new QuestionModel();
                            q.setId(d2.getId());
                            q.setDiff(d2.getDifficulty());
                            q.setQuestion(d2.getQuestion());
                            q.setCategory(d2.getCategory());
                            q.setSubcategory(d2.getSubcategory());
                            ques.add(q);
                        }
                    }
                }
                //Set values for the answer model
                ans.setAnswer(a1);
                ans.setQuestions(ques);
                ans.setSize(ques.size());
                //Add it to the Answers arraylist
                Answers.add(ans);
            }
        }

        System.out.println("Saving list");
        writeObject(Answers);
        System.out.println("Done saving list");

    }

    public static void writeObject(List<AnswerModel> listAccount) throws IOException {
        //Create FileOutputStream to write file
        FileOutputStream fos = new FileOutputStream("answersList.datum");
        //Create ObjectOutputStream to write object
        ObjectOutputStream objOutputStream = new ObjectOutputStream(fos);
        //Write object to file
        for (Object obj : listAccount) {

            objOutputStream.writeObject(obj);
            objOutputStream.reset();
        }
        objOutputStream.close();
    }
    public static ArrayList<AnswerModel> readObject() throws ClassNotFoundException, IOException {
        ArrayList<AnswerModel> listAccount = new ArrayList();
        //Create new FileInputStream object to read file
        FileInputStream fis = new FileInputStream("answersList.datum");
        //Create new ObjectInputStream object to read object from file
        ObjectInputStream obj = new ObjectInputStream(fis);
        try {
            while (fis.available() != -1) {
                //Read object from file
                AnswerModel acc = (AnswerModel) obj.readObject();
                listAccount.add(acc);
            }
        } catch (EOFException ex) {
            //ex.printStackTrace();
        }
        return listAccount;
    }
}
