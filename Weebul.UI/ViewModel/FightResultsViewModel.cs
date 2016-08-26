using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Helpers;
using Weebul.Core.Model;

namespace Weebul.UI.ViewModel
{
    public class FightResultsViewModel : ViewModelBase
    {

        public FightResultsViewModel()


        {
            if (IsInDesignModeStatic)
            {
                SetDesignData();
            }
        }

        private void SetDesignData()
        {
            Fight fight = WeblParser.ParseFight(_bigString);
            SetResults(fight);
        }
        public void SetResults(Fight fight)
        {
            this.ResultString = fight.ToString();
            var list = new List<FighterRound>();
            foreach (Round res in fight.RoundResults)
            {
                list.Add(res.Fighter1Round);
                list.Add(res.Fighter2Round);
            }
            this.FighterRounds = list;
            this.Fight = fight; 
        }

        /// 
        private List<FighterRound> _fighterRounds = null;

        /// <summary>
        /// Sets and gets the MyProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<FighterRound> FighterRounds
        {
            get
            {
                return _fighterRounds;
            }

            set
            {
                if (_fighterRounds == value)
                {
                    return;
                }

                _fighterRounds = value;
                OnPropertyChanged();
            }
        }

        /// 
        private string _resultString = null;

        /// <summary>
        /// Sets and gets the ResultString property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ResultString
        {
            get
            {
                return _resultString;
            }

            set
            {
                if (_resultString == value)
                {
                    return;
                }

                _resultString = value;
                OnPropertyChanged();
            }
        }


        public Fight Fight { get; set; }
        #region bigString
        const string _bigString = @"

In this corner, standing 5 feet and 6 inches (168 centimeters) tall weighing in at 137 pounds (62 kilograms) with a record of 0 wins, 0 draws, and 0 losses is Test B 0001!!

In this corner, standing 5 feet and 6 inches (168 centimeters) tall weighing in at 137 pounds (62 kilograms) with a record of 0 wins, 0 draws, and 0 losses is ZZTest B 0001!!

This bout is scheduled for 12 rounds.


ROUND 1


Test B 0001 has 140.0 endurance points remaining.
Your tactics: aggressiveness = 3.4, power = 8.0, defense = 8.0, resting 0.6
Your AGG is reduced by clinching. You are trying to wear him out with body blows.



ZZTest B 0001 has 140.0 endurance points remaining.
Your tactics: aggressiveness = 3.4, power = 8.0, defense = 8.0, resting 0.6
Your AGG is reduced by clinching. You are trying to wear him out with body blows.


Test B 0001 is clinching a lot (clinching) and goes to the body.
ZZTest B 0001 is clinching a lot (clinching) and goes to the body.

...
...
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
...
The referee warns Test B 0001 about holding and hitting.
...
...
...
Test B 0001 tries to land a roundhouse to the head, but ZZTest B 0001 falls into a clinch .
ZZTest B 0001 tries to attack, but Test B 0001 hangs on until the referee separates them.
...
...
ZZTest B 0001 tries to attack, but Test B 0001 hangs on until the referee separates them.
...
ZZTest B 0001 launches an uppercut to the ribs, but Test B 0001 ties him up .
...
Test B 0001 attempts a roundhouse to the stomach, but ZZTest B 0001 falls into a clinch .
...
ZZTest B 0001 launches an uppercut to the ribs, but falls short .
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
...
ZZTest B 0001 tries a right to the jaw, but Test B 0001 hangs on until the referee breaks them up .
...
...
...
Test B 0001 throws a left to the stomach, but ZZTest B 0001 ties him up .
...
...
...
Test B 0001 connects with a roundhouse to the ribs. 
ZZTest B 0001 probes with a hook to the ribs, but Test B 0001 hangs on until the referee breaks them up .
Test B 0001 throws a hook to the solar plexus, but ZZTest B 0001 hangs on until the referee breaks them up .
Test B 0001 attacks with an uppercut to the jaw, but ZZTest B 0001 ties him up .
ZZTest B 0001 launches a hook to the head, but Test B 0001 falls into a clinch .
...
Test B 0001 tries to close, but ZZTest B 0001 ties him up.
Test B 0001 annoys ZZTest B 0001 with a straight right to the ribs. 
Test B 0001 punches ZZTest B 0001 with an overhand right to the stomach. 
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
ZZTest B 0001 annoys Test B 0001 with a straight right to the ribs. 
ZZTest B 0001 connects with a quick hook to the stomach. 
...
...
...
...
...
ZZTest B 0001 tries to attack, but Test B 0001 hangs on until the referee separates them.
ZZTest B 0001 fires an uppercut to the ribs. 
...
...
...
ZZTest B 0001 tries a hook to the face, but Test B 0001 ties him up .
...

BELL!

According to the Commentators: 

Test B 0001 landed 10 of 30 punches -- 8 power punches, 0 jabs, 2 rights. (38 points)
ZZTest B 0001 landed 10 of 33 punches -- 8 power punches, 0 jabs, 2 rights. (38 points)

This round was a 10-10 tie. 

The fight is too close to call at this point.


Test B 0001 doesn't need to rest.

ZZTest B 0001 doesn't need to rest.

Test B 0001:

Your fighter lost 6.6 points of endurance this round due to damage, and 0.4 points due to fatigue. He took 4.4 points of stun damage this round. He has accumulated 6 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 6.6 points of endurance this round due to damage, and 0.4 points due to fatigue. He took 4.4 points of stun damage this round. He has accumulated 6 points of damage in the fight. 


ROUND 2


Test B 0001 has 133.7 endurance points remaining.
Your tactics: aggressiveness = 3.4, power = 8.0, defense = 8.0, resting 0.6
Your AGG is reduced by clinching. You are trying to wear him out with body blows.



ZZTest B 0001 has 133.7 endurance points remaining.
Your tactics: aggressiveness = 3.4, power = 8.0, defense = 8.0, resting 0.6
Your AGG is reduced by clinching. You are trying to wear him out with body blows.


Test B 0001 is clinching a lot (clinching) and goes to the body.
ZZTest B 0001 is clinching a lot (clinching) and goes to the body.

Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
...
Test B 0001 tries a sweeping right to the ribs, but it's too slow .
Test B 0001 tries to land a hook to the chest, but ZZTest B 0001 hangs on until the referee breaks them up .
ZZTest B 0001 tries to close, but Test B 0001 ties him up.
Test B 0001 charges with an uppercut to the solar plexus, but misses completely .
Test B 0001 probes with a hook to the chin, but ZZTest B 0001 falls into a clinch .
...
The referee admonishes ZZTest B 0001 to stop clinching.
ZZTest B 0001 tries to close, but Test B 0001 ties him up.
ZZTest B 0001 connects with a quick hook to the solar plexus. 
Test B 0001 attacks with a hook to the solar plexus, but ZZTest B 0001 ties him up .
ZZTest B 0001 tries to land an uppercut to the stomach, but Test B 0001 ties him up .
...
...
...
...
...
Test B 0001 connects with a quick right to the stomach. 
...
...
Test B 0001 punches ZZTest B 0001 with an uppercut to the stomach. 
Test B 0001 tries to land an uppercut to the stomach, but ZZTest B 0001 falls into a clinch .
ZZTest B 0001 tries to land a hook to the stomach, but Test B 0001 falls into a clinch .
...
...
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
...
...
ZZTest B 0001 throws an uppercut to the stomach, but Test B 0001 hangs on until the referee breaks them up .
...
ZZTest B 0001 tries to close, but Test B 0001 ties him up.
ZZTest B 0001 probes with a cross to the stomach, but Test B 0001 falls into a clinch .
Test B 0001 hits ZZTest B 0001 with a quick uppercut to the stomach. 
ZZTest B 0001 takes charge with a sweeping right to the chest. 
...
ZZTest B 0001 throws a roundhouse to the ribs. 
...
...
...
...
...
...
...
...
...
ZZTest B 0001 tries to attack, but Test B 0001 hangs on until the referee separates them.
...
...
ZZTest B 0001 throws an uppercut to the solar plexus, but Test B 0001 ties him up .
...
ZZTest B 0001 reaches with a roundhouse to the ribs, but Test B 0001 falls into a clinch .

BELL!

According to the Commentators: 

Test B 0001 landed 10 of 33 punches -- 8 power punches, 0 jabs, 2 rights. (38 points)
ZZTest B 0001 landed 10 of 32 punches -- 8 power punches, 0 jabs, 2 rights. (38 points)

This round was a 10-10 tie. 

The fight is too close to call at this point.


Test B 0001 doesn't need to rest.

ZZTest B 0001 doesn't need to rest.

Test B 0001:

Your fighter lost 6.5 points of endurance this round due to damage, and 0.4 points due to fatigue. He took 4.3 points of stun damage this round. He has accumulated 11 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 6.5 points of endurance this round due to damage, and 0.4 points due to fatigue. He took 4.3 points of stun damage this round. He has accumulated 11 points of damage in the fight. 


ROUND 3


Test B 0001 has 128.3 endurance points remaining.
Your tactics: aggressiveness = 3.4, power = 8.0, defense = 8.0, resting 0.6
Your AGG is reduced by clinching. You are trying to wear him out with body blows.



ZZTest B 0001 has 128.3 endurance points remaining.
Your tactics: aggressiveness = 3.4, power = 8.0, defense = 8.0, resting 0.6
Your AGG is reduced by clinching. You are trying to wear him out with body blows.


Test B 0001 is clinching a lot (clinching) and goes to the body.
ZZTest B 0001 is clinching a lot (clinching) and goes to the body.

Test B 0001 tags ZZTest B 0001 with an uppercut to the solar plexus. 
Test B 0001 tries to land an uppercut to the eye, but ZZTest B 0001 hangs on until the referee breaks them up .
...
...
ZZTest B 0001 tries to land an overhand right to the ribs, but misses .
Test B 0001 throws a quick right hand to the chest. 
...
ZZTest B 0001 tries an uppercut to the ribs, but it's short .
...
Test B 0001 probes with a sweeping right to the chest, but falls short .
The referee admonishes ZZTest B 0001 to stop clinching.
...
...
...
...
ZZTest B 0001 tries to attack, but Test B 0001 hangs on until the referee separates them.
...
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
...
Test B 0001 reaches with an uppercut to the solar plexus, but it's short .
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
...
ZZTest B 0001 tries to attack, but Test B 0001 hangs on until the referee separates them.
...
ZZTest B 0001 tags Test B 0001 with an uppercut to the stomach. 
Test B 0001 fires an uppercut to the head. 
...
...
ZZTest B 0001 tries a roundhouse to the stomach, but Test B 0001 hangs on until the referee breaks them up .
...
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
ZZTest B 0001 lands a left to the stomach. 
...
...
Test B 0001 tries to land a roundhouse to the stomach, but goes wide .
The referee admonishes Test B 0001 to stop clinching.
ZZTest B 0001 tries to attack, but Test B 0001 hangs on until the referee separates them.
...
ZZTest B 0001 lunges with an uppercut to the stomach, but flails uselessly .
...
...
...
...
ZZTest B 0001 strikes with a hook to the solar plexus. 
...
...
ZZTest B 0001 throws a left to the nose, but Test B 0001 ties him up .
ZZTest B 0001 lashes out with a hook to the chest, but Test B 0001 hangs on until the referee breaks them up .
Test B 0001 attempts an uppercut to the solar plexus, but comes up empty .
...
...
...
...

BELL!

According to the Commentators: 

Test B 0001 landed 10 of 31 punches -- 8 power punches, 0 jabs, 2 rights. (38 points)
ZZTest B 0001 landed 10 of 34 punches -- 8 power punches, 0 jabs, 2 rights. (38 points)

This round was a 10-10 tie. 

The fight is too close to call at this point.


Test B 0001 doesn't need to rest.

ZZTest B 0001 doesn't need to rest.

Test B 0001:

Your fighter lost 6.4 points of endurance this round due to damage, and 0.4 points due to fatigue. He took 4.2 points of stun damage this round. He has accumulated 16 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 6.4 points of endurance this round due to damage, and 0.4 points due to fatigue. He took 4.2 points of stun damage this round. He has accumulated 16 points of damage in the fight. 


ROUND 4


Test B 0001 has 123.6 endurance points remaining.
Your tactics: aggressiveness = 7.0, power = 5.0, defense = 8.0, resting 0.0



ZZTest B 0001 has 123.6 endurance points remaining.
Your tactics: aggressiveness = 7.0, power = 5.0, defense = 8.0, resting 0.0


Test B 0001 dances around the ring (using the ring).
ZZTest B 0001 dances around the ring (using the ring).

Test B 0001 throws an overhand right to the head. 
Test B 0001 tries a roundhouse to the eye, but ZZTest B 0001 steps aside .
ZZTest B 0001 connects with a jab to the stomach. Test B 0001 sneers!
ZZTest B 0001 launches a jab to the mouth, but Test B 0001 narrowly avoids it .
...
ZZTest B 0001 connects with a quick jab to the face. 
Test B 0001 tries to land a right to the face, but goes wide .
Test B 0001 tags ZZTest B 0001 with a jab to the mouth. ZZTest B 0001 sneers!
ZZTest B 0001 lunges with an overhand right to the face, but can't connect .
ZZTest B 0001 tries to land a hook to the head, but Test B 0001 ducks .
...
ZZTest B 0001 tries a cross to the stomach, but Test B 0001 retreats .
Test B 0001 probes with a jab to the solar plexus, but ZZTest B 0001 avoids it .
Test B 0001 annoys ZZTest B 0001 with a quick jab to the stomach. 
...
Test B 0001 tries an uppercut to the ribs, but it's soft .
ZZTest B 0001 hits with a hook to the solar plexus. 
...
Test B 0001 throws a quick jab to the ribs. 
ZZTest B 0001 annoys Test B 0001 with a jab to the nose. Test B 0001 sneers!
ZZTest B 0001 tries to land a jab to the jaw, but Test B 0001 steps aside .
Test B 0001 throws a left to the ribs. ZZTest B 0001 sneers!
Test B 0001 throws a cross to the stomach, but ZZTest B 0001 dodges .
Test B 0001 throws a jab to the chin, but ZZTest B 0001 narrowly avoids it .
Test B 0001 lunges with a jab to the solar plexus, but ZZTest B 0001 backpedals .
...
Test B 0001 fires an uppercut to the stomach. 
ZZTest B 0001 throws a jab to the eye. 
ZZTest B 0001 throws a cross to the solar plexus, but Test B 0001 avoids it .
ZZTest B 0001 lands a sweeping right to the head. 
...
ZZTest B 0001 connects with a quick hook to the eye. 
Test B 0001 throws a jab to the face. 
ZZTest B 0001 throws a sweeping right to the chest. 
...
Test B 0001 reaches with a jab to the face, but ZZTest B 0001 dodges .
Test B 0001 fires a cross to the stomach, but ZZTest B 0001 retreats .
...
Test B 0001 charges with a jab to the mouth, but is ineffective .
...
ZZTest B 0001 reaches with a jab to the temple, but Test B 0001 escapes .
Test B 0001 throws a quick straight right to the nose. ZZTest B 0001 ignores it.
...
ZZTest B 0001 lashes out with a jab to the nose, but comes up empty .
...

BELL!

According to the Commentators: 

Test B 0001 landed 31 of 70 punches -- 6 power punches, 19 jabs, 6 rights. (80 points)
ZZTest B 0001 landed 31 of 63 punches -- 6 power punches, 19 jabs, 6 rights. (80 points)

This round was a 10-10 tie. 

The fight is too close to call at this point.


Test B 0001 remains standing while the trainer wipes him down.

ZZTest B 0001 remains standing while the trainer wipes him down.

Test B 0001:

Your fighter lost 4.7 points of endurance this round due to damage, and 3.5 points due to fatigue. He took 4.7 points of stun damage this round. He has accumulated 21 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 4.7 points of endurance this round due to damage, and 3.5 points due to fatigue. He took 4.7 points of stun damage this round. He has accumulated 21 points of damage in the fight. 


ROUND 5


Test B 0001 has 117.9 endurance points remaining.
Your tactics: aggressiveness = 7.0, power = 5.0, defense = 8.0, resting 0.0



ZZTest B 0001 has 117.9 endurance points remaining.
Your tactics: aggressiveness = 7.0, power = 5.0, defense = 8.0, resting 0.0


Test B 0001 dances around the ring (using the ring).
ZZTest B 0001 dances around the ring (using the ring).

Test B 0001 throws a jab to the temple, but fails to score .
Test B 0001 connects with a quick cross to the eye. ZZTest B 0001 ignores it.
...
Test B 0001 lunges with a jab to the eye, but ZZTest B 0001 retreats .
Test B 0001 lands a roundhouse to the ribs. 
...
ZZTest B 0001 charges with an uppercut to the jaw, but Test B 0001 slips it .
...
ZZTest B 0001 lands an uppercut to the ribs. 
ZZTest B 0001 tries to land a hook to the ribs, but doesn't quite connect .
Test B 0001 throws a quick straight right to the head. 
Test B 0001 lashes out with an uppercut to the mouth, but ZZTest B 0001 leaps aside .
ZZTest B 0001 annoys Test B 0001 with a jab to the mouth. 
ZZTest B 0001 throws a cross to the head. Test B 0001 sneers!
Test B 0001 throws a roundhouse to the eye, but ZZTest B 0001 quickly backs away .
...
ZZTest B 0001 annoys Test B 0001 with a quick jab to the eye. 
Test B 0001 charges with a sweeping right to the chest, but ZZTest B 0001 steps aside .
Test B 0001 lashes out with a sweeping right to the chest, but ZZTest B 0001 leaps aside .
Test B 0001 launches a jab to the head, but ZZTest B 0001 backpedals .
Test B 0001 launches a jab to the stomach, but ZZTest B 0001 retreats .
Test B 0001 lunges with a straight right to the temple, but fails to score .
Test B 0001 connects with a jab to the nose. 
ZZTest B 0001 charges with a jab to the stomach, but only touches with it .
Test B 0001 connects with a jab to the chest. ZZTest B 0001 ignores it.
ZZTest B 0001 throws a quick jab to the chest. Test B 0001 sneers!
Test B 0001 tags ZZTest B 0001 with a roundhouse to the stomach. 
...
...
ZZTest B 0001 attacks with a jab to the jaw, but misses completely .
ZZTest B 0001 annoys Test B 0001 with a jab to the stomach. 
ZZTest B 0001 probes with a right hand to the mouth, but Test B 0001 steps aside .
...
...
...
Test B 0001 throws a jab to the ribs. 
ZZTest B 0001 tries to land a cross to the temple, but Test B 0001 backpedals .
...
ZZTest B 0001 probes with a jab to the eye, but Test B 0001 leaps aside .
Test B 0001 connects with a quick jab to the nose. ZZTest B 0001 sneers!
ZZTest B 0001 attacks with a jab to the head, but Test B 0001 evades it .
ZZTest B 0001 lands a quick left to the stomach. 
Test B 0001 attacks with a jab to the chest, but ZZTest B 0001 slips it .
...
ZZTest B 0001 fires a clean uppercut to the chin. 

BELL!

According to the Commentators: 

Test B 0001 landed 31 of 70 punches -- 6 power punches, 19 jabs, 6 rights. (80 points)
ZZTest B 0001 landed 31 of 64 punches -- 6 power punches, 19 jabs, 6 rights. (80 points)

This round was a 10-10 tie. 

The fight is too close to call at this point.


Test B 0001 remains standing while the trainer wipes him down.

ZZTest B 0001 remains standing while the trainer wipes him down.

Test B 0001:

Your fighter lost 4.6 points of endurance this round due to damage, and 3.5 points due to fatigue. He took 4.6 points of stun damage this round. He has accumulated 25 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 4.6 points of endurance this round due to damage, and 3.5 points due to fatigue. He took 4.6 points of stun damage this round. He has accumulated 25 points of damage in the fight. 


ROUND 6


Test B 0001 has 112.9 endurance points remaining.
Your tactics: aggressiveness = 7.0, power = 5.0, defense = 8.0, resting 0.0



ZZTest B 0001 has 112.9 endurance points remaining.
Your tactics: aggressiveness = 7.0, power = 5.0, defense = 8.0, resting 0.0


Test B 0001 dances around the ring (using the ring).
ZZTest B 0001 dances around the ring (using the ring).

Test B 0001 reaches with a sweeping right to the head, but it's short .
ZZTest B 0001 throws a quick jab to the head. 
ZZTest B 0001 annoys Test B 0001 with a quick cross to the stomach. 
ZZTest B 0001 lands a sweeping right to the stomach. 
...
ZZTest B 0001 probes with a cross to the chest, but Test B 0001 slips it .
Test B 0001 throws a quick jab to the chest. 
Test B 0001 connects with a quick cross to the eye. ZZTest B 0001 sneers!
Test B 0001 tags ZZTest B 0001 with an overhand right to the nose. 
ZZTest B 0001 throws a jab to the chin. 
...
ZZTest B 0001 launches a jab to the face, but Test B 0001 steps aside .
ZZTest B 0001 hits Test B 0001 with an overhand right to the solar plexus. 
Test B 0001 annoys ZZTest B 0001 with a jab to the chin. 
...
ZZTest B 0001 tries to land a jab to the face, but falls short .
Test B 0001 launches a jab to the jaw, but ZZTest B 0001 steps aside .
ZZTest B 0001 lashes out with a left to the solar plexus, but Test B 0001 deftly avoids it .
Test B 0001 attempts an uppercut to the temple, but ZZTest B 0001 ducks .
Test B 0001 throws a quick jab to the ribs. 
...
ZZTest B 0001 throws an overhand right to the stomach, but Test B 0001 escapes .
...
Test B 0001 lands a right hand to the ribs. 
...
ZZTest B 0001 charges with a hook to the jaw, but is ineffective .
...
Test B 0001 launches a jab to the face, but ZZTest B 0001 ducks .
Test B 0001 fires a jab to the head, but ZZTest B 0001 dodges .
Test B 0001 charges with a roundhouse to the eye, but ZZTest B 0001 leaps aside .
...
...
ZZTest B 0001 connects with a quick jab to the mouth. 
ZZTest B 0001 fires a jab to the head, but Test B 0001 backpedals .
ZZTest B 0001 annoys Test B 0001 with a hook to the stomach. 
...
Test B 0001 tries a straight right to the solar plexus, but falls short .
ZZTest B 0001 reaches with a jab to the stomach, but falls short .
Test B 0001 reaches with a jab to the mouth, but ZZTest B 0001 escapes .
...
ZZTest B 0001 connects with a quick jab to the head. 
ZZTest B 0001 launches a jab to the stomach, but Test B 0001 avoids it .
Test B 0001 throws a quick jab to the head. 
...
Test B 0001 annoys ZZTest B 0001 with a hook to the head. 

BELL!

According to the Commentators: 

Test B 0001 landed 30 of 62 punches -- 6 power punches, 18 jabs, 6 rights. (78 points)
ZZTest B 0001 landed 30 of 67 punches -- 6 power punches, 18 jabs, 6 rights. (78 points)

This round was a 10-10 tie. 

The fight is too close to call at this point.


Test B 0001 remains standing while the trainer wipes him down.

ZZTest B 0001 remains standing while the trainer wipes him down.

Test B 0001:

Your fighter lost 4.5 points of endurance this round due to damage, and 3.5 points due to fatigue. He took 4.5 points of stun damage this round. He has accumulated 30 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 4.5 points of endurance this round due to damage, and 3.5 points due to fatigue. He took 4.5 points of stun damage this round. He has accumulated 30 points of damage in the fight. 


ROUND 7


Test B 0001 has 108.4 endurance points remaining.
Your tactics: aggressiveness = 7.0, power = 5.0, defense = 8.0, resting 0.0



ZZTest B 0001 has 108.4 endurance points remaining.
Your tactics: aggressiveness = 7.0, power = 5.0, defense = 8.0, resting 0.0


Test B 0001 dances around the ring (using the ring).
ZZTest B 0001 dances around the ring (using the ring).

...
ZZTest B 0001 launches a jab to the eye, but is ineffective .
...
...
...
ZZTest B 0001 launches an uppercut to the ribs, but Test B 0001 steps aside .
ZZTest B 0001 throws a roundhouse to the head. 
...
ZZTest B 0001 lashes out with an uppercut to the solar plexus, but Test B 0001 deftly avoids it .
Test B 0001 connects with a quick jab to the eye. 
Test B 0001 tries to land a left to the temple, but ZZTest B 0001 slips it .
...
ZZTest B 0001 tries to land a jab to the mouth, but Test B 0001 escapes .
Test B 0001 hits ZZTest B 0001 with a left to the jaw. 
ZZTest B 0001 lands a jab to the ribs. 
ZZTest B 0001 annoys Test B 0001 with a jab to the chest. 
Test B 0001 probes with a jab to the head, but ZZTest B 0001 deftly avoids it .
ZZTest B 0001 launches a left to the head, but flails uselessly .
Test B 0001 throws a quick jab to the solar plexus. 
ZZTest B 0001 tries a right hand to the head, but can't connect .
...
...
Test B 0001 connects with a clean jab to the temple. 
Test B 0001 connects with a hook to the chest. 
Test B 0001 hits with a roundhouse to the temple. 
ZZTest B 0001 tries a right hand to the head, but Test B 0001 ducks .
ZZTest B 0001 lands a quick jab to the solar plexus. 
ZZTest B 0001 annoys Test B 0001 with a quick overhand right to the head. Test B 0001 sneers!
...
...
...
ZZTest B 0001 reaches with a jab to the mouth, but Test B 0001 escapes .
...
...
Test B 0001 fires an uppercut to the ribs, but ZZTest B 0001 narrowly avoids it .
ZZTest B 0001 attempts a jab to the stomach, but Test B 0001 quickly backs away .
Test B 0001 throws a jab to the stomach, but ZZTest B 0001 backpedals .
ZZTest B 0001 fires a right hand to the eye. 
Test B 0001 attacks with a left to the nose, but is ineffective .
Test B 0001 throws a jab to the ribs. 
ZZTest B 0001 annoys Test B 0001 with a quick jab to the ribs. 
ZZTest B 0001 lunges with a jab to the nose, but Test B 0001 backpedals .
Test B 0001 throws a jab to the ribs, but comes up empty .
...
...

BELL!

According to the Commentators: 

Test B 0001 landed 28 of 53 punches -- 5 power punches, 18 jabs, 5 rights. (71 points)
ZZTest B 0001 landed 28 of 67 punches -- 5 power punches, 18 jabs, 5 rights. (71 points)

This round was a 10-10 tie. 

The fight is too close to call at this point.


Test B 0001 grabs a water bottle and rests on his stool.

ZZTest B 0001 grabs a water bottle and rests on his stool.

Test B 0001:

Your fighter lost 4.4 points of endurance this round due to damage, and 3.5 points due to fatigue. He took 4.4 points of stun damage this round. He has accumulated 34 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 4.4 points of endurance this round due to damage, and 3.5 points due to fatigue. He took 4.4 points of stun damage this round. He has accumulated 34 points of damage in the fight. 


ROUND 8


Test B 0001 has 104.5 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0



ZZTest B 0001 has 104.5 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0


Test B 0001 is wading through punches! (all out).
ZZTest B 0001 is wading through punches! (all out).

...
Test B 0001 throws a roundhouse to the temple, but ZZTest B 0001 slips it .
ZZTest B 0001 lashes out with an uppercut to the face, but Test B 0001 knocks it away .
...
Test B 0001 jars ZZTest B 0001 with a big overhand right to the jaw!! The crowd is on its feet!! ZZTest B 0001 staggers!! The crowd is going wild!!
...
ZZTest B 0001 takes charge with a hook to the temple. 
ZZTest B 0001 lunges with a right hand to the chest, but it's short .
Test B 0001 jars ZZTest B 0001 with a big hook to the stomach!! The crowd is on its feet!! ZZTest B 0001 is hurt! He barely keeps his feet under him!
ZZTest B 0001 jars Test B 0001 with a powerful right hand to the stomach!! The crowd is on its feet!! 

ZZTest B 0001 lands a monster straight and Test B 0001 collapses!! 

ONE! 

TWO! 

THREE! 

FOUR! 

FIVE! 

SIX! 

SEVEN! 

EIGHT! 

NINE! 

Test B 0001 doesn't look like he'll beat the count -- but he does! and the fight goes on!

ZZTest B 0001 jars Test B 0001 with a big hook to the mouth!! The crowd is on its feet!! 
...
...

Test B 0001 lands a telling straight and ZZTest B 0001 lands flat on his back!! 

ONE! 

TWO! 

THREE! 

FOUR! 

FIVE! 

SIX! 

SEVEN! 

EIGHT! 

NINE! 

ZZTest B 0001 doesn't look like he'll beat the count -- but he does! and the fight goes on!

...
ZZTest B 0001 jars Test B 0001 with a big uppercut to the eye!! The crowd is on its feet!! 
...
...
ZZTest B 0001 annoys Test B 0001 with a quick hook to the stomach. Test B 0001 staggers!! The crowd is going wild!!
Test B 0001 tries to land a right hand to the mouth, but falls short .
ZZTest B 0001 tags Test B 0001 with a quick hook to the stomach. Test B 0001 staggers!! The crowd is going wild!!
ZZTest B 0001 annoys Test B 0001 with a right to the ribs. Test B 0001 sneers!
...
...
...
...

Test B 0001 lands a ferocious combination and ZZTest B 0001 lands flat on his back!! 

ONE! 

TWO! 

THREE! 

FOUR! 

FIVE! 

SIX! 

SEVEN! 

EIGHT! 

NINE! 

ZZTest B 0001 doesn't look like he'll beat the count -- but he does! and the fight goes on!

ZZTest B 0001 throws a quick uppercut to the solar plexus. Test B 0001 seems wobbly!! It looks like he might go down..
ZZTest B 0001 throws a cross to the temple. 

ZZTest B 0001 lands a telling combination and Test B 0001's knees buckle --- he topples to the canvass!! 

ONE! 

TWO! 

THREE! 

FOUR! 

FIVE! 

SIX! 

SEVEN! 

EIGHT! 

NINE! 

Test B 0001 doesn't look like he'll beat the count -- but he does! and the fight goes on!

...

ZZTest B 0001 annoys Test B 0001 with an uppercut to the stomach. 
ZZTest B 0001 throws a quick overhand right to the stomach. 

ZZTest B 0001 lands a big right bomb and Test B 0001 staggers. He reaches for the ropes ... and misses!! 

ONE! 

TWO! 

THREE! 

FOUR! 

FIVE! 

SIX! 

SEVEN! 

EIGHT! 

NINE! 

TEN! 
He's out!!!
ZZTest B 0001 wins by a Knock Out!!

Time: 1:25

Test B 0001 can't remember which corner is his. He has swelling around his left eye. He has swelling around his right eye. He has bleeding over his right eye. He has a fractured nose. He has a bloody lip.

ZZTest B 0001 collapses limply onto his stool. He has swelling around his left eye.

Test B 0001:

Your fighter lost 102.4 points of endurance this round due to damage, and 4.0 points due to fatigue. He took 99.4 points of stun damage this round. He has accumulated 134 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 74.6 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 74.6 points of stun damage this round. He has accumulated 109 points of damage in the fight. 






Judge Roy Bean had the fight scored as follows: 

Round 1: A 10-10 tie.
Round 2: A 10-10 tie.
Round 3: A 10-10 tie.
Round 4: A 10-10 tie.
Round 5: A 10-10 tie.
Round 6: A 10-10 tie.
Round 7: A 10-10 tie.



Judge Judy had the fight scored as follows: 

Round 1: A 10-10 tie.
Round 2: A 10-10 tie.
Round 3: A 10-10 tie.
Round 4: A 10-10 tie.
Round 5: A 10-10 tie.
Round 6: A 10-10 tie.
Round 7: A 10-10 tie.



Judge Lao Mang Chen had the fight scored as follows: 

Round 1: A 10-10 tie.
Round 2: A 10-10 tie.
Round 3: A 10-10 tie.
Round 4: A 10-10 tie.
Round 5: A 10-10 tie.
Round 6: A 10-10 tie.
Round 7: A 10-10 tie.



";
        #endregion
    }
}