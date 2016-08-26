package boxingsim;

/**
 *
 * @author Darren Reifler
 */

import java.util.*;
import javax.swing.*;
import java.awt.BorderLayout;
import java.awt.event.*;
import java.awt.Font;
import java.awt.GridLayout;

public class BoxingSim {

    /**
     * @param args the command line arguments
     */

    public static void main(String[] args) {
        // TODO code application logic here
    //Create a frame to run applet
    gameWin main = new gameWin();
    //Display the frame
    main.setTitle("Boxing Sim");
    main.setSize(800,600);
    main.setLocation(200, 200);
    main.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    main.setVisible(true);
    main.validate();

    //Test data - create button links to open and close gym/fighter creation
    //within applet or in new applet to create and store info
    //create files with pre-made fighters
    //create AI for fighters with different styles etc.


    //Simulation thread
    Runnable Sim = new Sim(main);
    Thread thread1 = new Thread(Sim);
    main.run.addActionListener(new ActionListener()
            {   @Override
                public void actionPerformed(ActionEvent e)
                {
                    thread1.start();
                }
            });
    }
}

class Sim implements Runnable{
    gameWin main;
    boolean intro = false, both = false;
    int dec1, dec2;
    int time = 0, round = 1;
    Region test = new Region("Test");
    Gym green = new Gym("green", "Tester 1");
    Gym blue = new Gym("blue", "Tester 2");
    Slugger n1 = new Slugger("Slugger", green, 69);
    Dancer n2 = new Dancer("Dancer", blue, 72);
    String thrown1,thrown2,landed1,landed2,rnd;

    Sim(gameWin win){
        main = win;
    }
    @Override
    public void run() {

        if (intro == false){
            n1.assignRegion(test);
            test.addFighter(n1);
            n2.assignRegion(test);
            test.addFighter(n2);
        }

        if (intro == false){
            Introduce(n1,n2);
            intro = true;
        }
        main.rndT.setText("1");

        for(int i = 1; i <= 4; i++){
            time = 0;
            while (time < 181){
                //Add selection of which fighter goes first
                //Add what happens if both fighters punch
                //run decision method from fighter type
                dec1 = Decide(n1);
                //System.out.println("dec1 = " + dec1);
                dec2 = Decide(n2);
                //System.out.println("dec2 = " + dec2);
                Pause(100);
                if (dec1 == 1)
                    Rest(n1,n2);
                if (dec2 == 1)
                    Rest(n2,n1);
                if (dec1 == 3)
                    Defend(n1,n2);
                if (dec2 == 3)
                    Defend(n2,n1);
                //Add situation where both want to punch at same time
                if (dec1 == 2 && dec2 == 2)
                    both = punchTie(n1,n2);
                if (dec1 == 2)
                    Punch(n1, n2, both);
                if (dec2 == 2)
                    Punch(n2, n1, both);
                Pause(100);
                both = false;
                time++;
                System.out.println("sec = " + time);
                UpdatePunches(n1,n2);
                main.jta.setCaretPosition(main.jta.getDocument().getLength());
                }
            UpdateRound(n1,n2);
            round++;
            rnd = "" + round;
            if (round < 5){
                main.rndT.setText(rnd);
                main.jta.append("\nRound " + round + "\n");
                }
            }
        Thread.currentThread().interrupt();
    }

    public void UpdatePunches(Fighter n1, Fighter n2) {
        //update punches thrown and landed windows
        thrown1 = "" + n1.thrown;
            main.JpThr1.setText(thrown1);
        landed1 = "" + n1.landed;
            main.JpLnd1.setText(landed1);
        thrown2 = "" + n2.thrown;
            main.JpThr2.setText(thrown2);
        landed2 = "" + n2.landed;
            main.JpLnd2.setText(landed2);
    }

    public void UpdateRound(Fighter n1, Fighter n2) {
        //update punches thrown and landed windows
        thrown1 = "" + n1.thrownTtl + n1.thrown;
        n1.thrown = n1.thrownTtl + n1.thrown;
            main.JpThrTtl1.setText(thrown1);
        landed1 = "" + n1.landedTtl + n1.landed;
        n1.landed = n1.landedTtl + n1.landed;
            main.JpLndTtl1.setText(landed1);
        thrown2 = "" + n2.thrownTtl + n2.thrown;
            n2.thrown = n2.thrownTtl + n2.thrown;
            main.JpThrTtl2.setText(thrown2);
        landed2 = "" + n2.landedTtl + n2.landed;
            n2.landed = n2.landedTtl + n2.landed;
            main.JpLndTtl2.setText(landed2);
    }

    public void Introduce(Fighter n1, Fighter n2){
        main.jta.append("Introducing in the green corner...");
        Pause(1000);
        main.jta.append(n1.name + "!\n");
        Pause(1000);
        main.jta.append("and his opponent fighting out of the blue corner...");
        Pause(1000);
        main.jta.append(n2.name + "!\n");
    }

    public int Decide(Fighter act){
        int decide = 1;
        double rand, punch, move, defend;

        act.action = (act.jab + act.power)/2 + act.defense + act.agg;
        punch = ((act.jab + act.power)/2)/ act.action;
        defend = (((act.jab + act.power)/2) + act.defense)/act.action;

        //System.out.println("punch " + punch);
        //System.out.println("defend " + defend);
        rand = Math.random();
        //System.out.println("rand " + rand);

        if (rand < punch)
            decide = 2;
        else if (rand < defend)
            decide = 3;

        return decide;
    }

    public boolean punchTie(Fighter act, Fighter opp){
        boolean actPunch = true;
        double spdTtl, spdAct, rand;
        //Decide who throws punch if both is true\
            spdTtl = opp.spd + act.spd;
            spdAct = act.spd/spdTtl;
            rand = Math.random();
            if (spdAct < rand) {
                actPunch = false;
                main.jta.append(opp.name + " is beaten to the punch.\n");
            }
        return actPunch;
    }

    public void Punch(Fighter act, Fighter opp, boolean both){
        boolean jabCheck;
        double rand, pun, jab, pow, spdTtl,spdAct;
        String thrown;

        if (both = true){
        act.punch();
        //Decide jab or power punch
        act.action = act.jab + act.power;
        pun = act.jab/act.action;
        rand = Math.random();
        if (rand < pun)
            jabCheck = true;
        else jabCheck = false;

        //simulate jab
        if (jabCheck == true){
            //Add a counterpunch feature for opp to counter a thrown punch
            //Add a combination feature for act to follow a landed punch with a
            //power punch
            //Add: Factor in number of punches of each type thrown
            act.thrown++;
            //decide if jab lands (factor in distance later)
            act.action = (act.jab + act.spd/2) * (act.end/act.cnd);
            opp.action = (opp.defense + opp.agl/2) * (opp.end/opp.cnd);
            jab = act.action/(act.action + opp.action);
            System.out.println("jab = " + jab);
            System.out.println("rand = " + rand);
            rand = Math.random();
            if (rand < jab){
                main.jta.append(act.name + " lands a jab.\n");
                act.jabLand++;
                act.landed++;
                act.end = act.end - .1;
                act.end = act.end - .05;
            }
            else{
                if(opp.defending == true)
                    main.jta.append(act.name + " blocks a jab.\n");
                else
                    main.jta.append(act.name + " misses with a jab.\n");
                    }
        }

        //simulate power
        //Later add Body or head
        if (jabCheck == false){
            //Add a counterpunch feature for opp to counter a thrown punch
            //Add a combination feature for act to follow a landed punch with a
            //power punch
            //Add: Factor in number of punches of each type thrown

             act.thrown++;

            //Calculate if a power punch lands
            if (opp.defending == true)
               opp.action = ((opp.defense * 2) + opp.agl/2) * (opp.end/opp.cnd);
            else if (opp.resting == true)
                opp.action = ((opp.defense/2) + opp.agl/2) * (opp.end/opp.cnd);

            pow = act.action/(act.action + opp.action);
            rand = Math.random();
            System.out.println("act.action = " + act.action);
            System.out.println("opp.action = " + opp.action);
            if (rand < pow){
                main.jta.append(act.name + " lands a power punch.\n");
                act.powLand++;
                act.landed++;
                act.end = act.end - .1;
                //later calculate damage done by fighters stats
                act.end = act.end - .15;
            }
            else {
                if(opp.defending == true)
                    main.jta.append(opp.name + " blocks a power punch.\n");
                else
                    main.jta.append(act.name + " misses a power punch.\n");
            }
          }
        }
    }

    public void Defend(Fighter act, Fighter opp){
        act.defend();
    }

    public void Rest(Fighter act, Fighter opp){
        act.rest();
    }

    public void Pause(int i){
        try{
            Thread.sleep(i);
        }catch(InterruptedException e){}
    }
}

class gameWin extends JFrame{
    JTextArea jta = new JTextArea();
    JButton run = new JButton("Run Sim");

    //fields for punches thrown and thrown
    JPanel p1 = new JPanel();
    JPanel p2 = new JPanel();
    JLabel rnd = new JLabel("Round:");
    JTextField rndT = new JTextField(2);
    JLabel pThr1 = new JLabel("Green Thrown:");
    JTextField JpThr1 = new JTextField(3);
    JLabel pLnd1 = new JLabel("Green Landed:");
    JTextField JpLnd1 = new JTextField(3);
    JLabel pThr2 = new JLabel("Blue Thrown:");
    JTextField JpThr2 = new JTextField(3);
    JLabel pLnd2 = new JLabel("Blue Landed:");
    JTextField JpLnd2 = new JTextField(3);

    JLabel pThrTtl1 = new JLabel("Green Thrown:");
    JTextField JpThrTtl1 = new JTextField(3);
    JLabel pLndTtl1 = new JLabel("Green Landed:");
    JTextField JpLndTtl1 = new JTextField(3);
    JLabel pThrTtl2 = new JLabel("Blue Thrown:");
    JTextField JpThrTtl2 = new JTextField(3);
    JLabel pLndTtl2 = new JLabel("Blue Landed:");
    JTextField JpLndTtl2 = new JTextField(3);

    gameWin(){
        setLayout(new BorderLayout(5,5));
        p1.setLayout(new GridLayout(2,8,5,5));
        p2.setLayout(new GridLayout(1,3,100,5));
        JScrollPane scroll = new JScrollPane(jta);
        scroll.setVerticalScrollBarPolicy(ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS);
        p2.add(run);
        add(scroll,BorderLayout.CENTER);
        p2.add(rnd);
        p2.add(rndT);
        p1.add(pThr1);
        p1.add(JpThr1);
        p1.add(pLnd1);
        p1.add(JpLnd1);
        p1.add(pThr2);
        p1.add(JpThr2);
        p1.add(pLnd2);
        p1.add(JpLnd2);
        p1.add(pThrTtl1);
        p1.add(JpThrTtl1);
        p1.add(pLndTtl1);
        p1.add(JpLndTtl1);
        p1.add(pThrTtl2);
        p1.add(JpThrTtl2);
        p1.add(pLndTtl2);
        p1.add(JpLndTtl2);
        add(p2,BorderLayout.SOUTH);
        add(p1,BorderLayout.NORTH);


    }
}


class Gym{
    static int globalid = 0;
    int id, wins = 0, losses = 0, draws = 0, titles = 0;
    List<Fighter> gymFighters = new ArrayList<Fighter>();
    String name, owner;

    Gym(String n, String o){
        this.name = n;
        this.owner = o;
        this.id = globalid;
        ++globalid;
    }

    void addFighter(Fighter f){
        gymFighters.add(f);
    }

    void changeName(String n){
        this.name = n;
    }
}

class Region{
    String name, title;
    List<Fighter> regionFighters = new ArrayList<Fighter>();

    Region(String n){
        this.name = n;
    }

    //Create code to schedule fighters
    //Create code to print fighter rankings

    void addFighter(Fighter f){
        regionFighters.add(f);
    }
}

//A Class of NPC fighters for computer generated fighters
class Slugger extends Fighter {


    public Slugger(){

    }
    public Slugger(String n, Gym g, int h){
        this.name = n;
        this.gym = g;
        this.build = 5;//heavy build
        this.hgt = h;
        this.weight();
        startAbl();
    }

    //Randomly generating computer opponent
    public void startAbl(){
        super.str = 50 + ((int)Math.random() * 100)/2;
        super.agl = 30 + ((int)Math.random() * 100)/2;
        super.spd = 100 - agl;
        super.cnd = 50 + ((int)Math.random() * 100)/2;
        super.end = super.cnd;
        super.defense = 50;
        super.jab = 30;
        super.power = 90;
        super.agg = 50;
        super.calcHidden();
    }

    public String Action(){
        String action = "none";


        return action;
    }
}

 class Dancer extends Fighter {

    public Dancer(){
    }

    public Dancer(String n, Gym g, int h){
        this.name = n;
        this.gym = g;
        this.build = 1;//light build
        this.hgt = h;
        this.weight();
        startAbl();
    }

    //Randomly generating computer opponent
    public void startAbl(){
        super.str = 30 + ((int)Math.random() * 100)/2;
        super.agl = 50 + ((int)Math.random() * 100)/2;
        super.spd = 100 - agl;
        super.cnd = 50 + ((int)Math.random() * 100)/2;
        super.end = super.cnd;
        super.defense = 80;
        super.jab = 90;
        super.power = 30;
        super.agg = 70;
        super.calcHidden();
    }
}

//may create trainers/coaches to train skills
class Fighter{
    String name;
    Gym gym;
    Region region;
    //Shown fighter abilities
    static int globalid = 0;
    //strength and conditioning will be trainable up to a char specific limit
    //speed and agility will be set at the start
    int str, spd, agl, cnd, hgt, build, id;
    double end;
    double wgt, age = 18;
    //Trainable fighter skills (shown)
    int jab = 1, power = 1, body = 1, move = 1, control = 1, feint = 1;
    int slip = 1, defense = 1, counter = 1;
    //Hidden fighter stats
    //New stats of experience, knowledge, charisma--Will make more
    //Hidden stats determined randomly--maybe option to manipulate
    int chn, kp, cha;
    //Fighter ratings - Level knowledge and experience grow over time by
    //training and fighting. Status comes from bigger/more exciting fights
    int level = 1, status = 0, rating = 0, knw = 1, exp = 1;
    int wins = 0, losses = 0, draws = 0;
    boolean resting = false, punching = false, defending = true;
    boolean moveAway = false, moveClose = false;
    //Simulation parameters
    int distance = 2, jabLand = 0, powLand = 0, jabLandTtl = 0, powLandTtl = 0,
            agg = 70;
    int jabThrown = 0, powThrown = 0;
    int thrown = jabThrown + powThrown;
    int landed = jabLand + powLand;
    int jabThrownTtl = 0, powThrownTtl = 0;
    int thrownTtl = jabThrownTtl + powThrownTtl;
    int landedTtl = jabLandTtl + powLandTtl;
    double action;

    public Fighter(){

    }

    public Fighter(String n, Gym g){
        this.name = n;
        this.gym = g;
    }

    public void assignRegion(Region r){
        this.region = r;
    }

    //Calculate fighter weight
    public void weight(){
        wgt = 105;
        for (int i = 1; i <= (hgt - 60); i++)
           wgt = wgt * 1.0475;
        for (int i = 1; i <= str; i++)
            wgt = wgt * (1.01 + .0325/(1 + Math.abs(i - 10)));
        for (int i = 1; i <= agl; i++)
            wgt = wgt * (.99 - (.0250/(1 + Math.abs(i - 10))));
        wgt = wgt * (.94 + (.02 * build));
    }

    //Methods to randomly determine chin, KP, and charisma
    public void calcHidden(){
        this.chn = (int)Math.random() * 100;
        this.kp = (int)Math.random() * 100;
        this.cha = (int)Math.random() * 100;
    }

    public void rest(){
        this.resting = true;
        this.defending = false;
        this.punching = false;
        this.moveAway = false;
        this.moveClose = false;
    }

     public void punch(){
        this.resting = false;
        this.defending = false;
        this.punching = true;
        this.moveAway = false;
        this.moveClose = false;
    }

    public void defend(){
        this.resting = false;
        this.defending = true;
        this.punching = false;
        this.moveAway = false;
        this.moveClose = false;
    }

    public void moveAway(){
        this.resting = true;
        this.defending = false;
        this.punching = false;
        this.moveAway = false;
        this.moveClose = false;
    }

    public void moveClose(){
        this.resting = true;
        this.defending = false;
        this.punching = false;
        this.moveAway = false;
        this.moveClose = false;
    }

    //Getter and Setter methods for fighter ratings
    public int getLevel(){
        return level;
    }

    public void addLevel(){
        this.level++;
    }

    public int getStatus(){
        return status;
    }

    public void addStatus(int s){
        this.status = this.status + s;
    }

    public int getKnw(){
        return status;
    }

    public void addKnw(int k){
        this.knw = this.knw + k;
    }

    public int getExp(){
        return exp;
    }

    public void addExp(int e){
        this.exp = this.exp + e;
    }

    //Getter and Setter methods for fighter statistics
    public double getAge(){
        return age;
    }

    public void setAge(double a){
        this.age = a;
    }

     public int getBuild(){
        return build;
    }

    public void setBuild(int b){
        this.build = b;
    }

    public int getWins(){
        return wins;
    }

    public void addWin(){
        this.wins++;
    }

    public int getLosses(){
        return losses;
    }

    public void addLoss(){
        this.losses++;
    }

    public int getDraws(){
        return draws;
    }

    public void addDraw(){
        this.draws++;
    }

    //Getter and Setter methods for all abilities and skills
    public int getCha(){
        return cha;
    }

    public void setCha(int c){
        this.cha = c;
    }

    public double getWgt() {
        return wgt;
    }

    public int getHgt() {
        return hgt;
    }

    public void setHgt(int h){
        this.hgt = h;
    }

    public int getStr() {
        return str;
    }

    public void setStr(int s){
        this.str = s;
    }

    public int getSpd() {
        return spd;
    }

    public void setSpd(int s){
        this.spd = s;
    }

    public int getAgl() {
        return agl;
    }

     public void setAgl(int a){
        this.agl = a;
    }

    public int getCnd() {
        return cnd;
    }

    public void setCnd(int c){
        this.cnd = c;
    }

    public int getChn() {
        return chn;
    }

    public void setChn(int c){
        this.chn = c;
    }

    public int getKp() {
        return kp;
    }

    public void setKP(int k){
        this.kp = k;
    }

    public int getJab(){
        return jab;
    }

    public void setJab(int j){
        this.jab = j;
    }

     public int getPower(){
        return power;
    }

    public void setPower(int p){
        this.power = p;
    }

    public int getBody(){
        return body;
    }

    public void setBody(int p){
        this.body = p;
    }

    public int getMove(){
        return move;
    }

    public void setMove(int m){
        this.move = m;
    }

    public int getFeint(){
        return feint;
    }

    public void setFeint(int f){
        this.feint = f;
    }

    public int getSlip(){
        return slip;
    }

    public void setSlip(int s){
        this.slip = s;
    }

    public int getDefense(){
        return defense;
    }

    public void setDefense(int d){
        this.defense = d;
    }

    public int getCounter(){
        return counter;
    }

    public void setCounter(int c){
        this.counter = c;
    }
}


