using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Helpers.Tests
{
    public partial class WeblParserTests
    {




        const string _roundText = @"ROUND 4


Test B 0001 has 124.9 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.



ZZTest B 0001 has 125.7 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.


Test B 0001 comes out fighting and jabs for the cut.
ZZTest B 0001 comes out fighting and jabs for the cut.

...
...
ZZTest B 0001 scores with a clean right hand to the eye. 
...
Test B 0001 lashes out with a hook to the eye, but ZZTest B 0001 hides behind his gloves .
ZZTest B 0001 takes charge with a sharp hook to the face. Test B 0001 covers up.
ZZTest B 0001 tries to land a hook to the mouth, but Test B 0001 evades it .
...
...
...
Test B 0001 clocks ZZTest B 0001 with a solid uppercut to the eye. 
...
ZZTest B 0001 lashes out with a left to the eye, but doesn't land it very well .
ZZTest B 0001 lands a quick roundhouse to the mouth. Test B 0001 ignores it.
...
Test B 0001 lashes out with a hook to the eye, but ZZTest B 0001 covers himself well .
Test B 0001 smacks ZZTest B 0001 with a stiff sweeping right to the eye. 
Test B 0001 throws an overhand right to the mouth. ZZTest B 0001 sneers!
Test B 0001 tags ZZTest B 0001 with a hook to the nose. 
...
...
...
...
ZZTest B 0001 hits with a hook to the face. 
...
ZZTest B 0001 annoys Test B 0001 with a straight right to the face. 
...
Test B 0001 attempts an uppercut to the face, but ZZTest B 0001 hides behind his gloves .
Test B 0001 throws a right to the eye. 
...
...
...
ZZTest B 0001 fires a hook to the mouth. 
ZZTest B 0001 fires an uppercut to the face, but Test B 0001 covers up .
ZZTest B 0001 probes with a right to the eye, but Test B 0001 hides behind his gloves .
...
...
...
Test B 0001 throws a straight right to the eye, but ZZTest B 0001 ducks .
...
...
Test B 0001 tries to land a right hand to the mouth, but it's too slow .
...
ZZTest B 0001 lunges with a hook to the nose, but falls short .
Test B 0001 annoys ZZTest B 0001 with a roundhouse to the nose. 

BELL!

According to the Commentators: 

Test B 0001 landed 24 of 45 punches -- 16 power punches, 0 jabs, 8 rights. (88 points)
ZZTest B 0001 landed 24 of 44 punches -- 16 power punches, 0 jabs, 8 rights. (88 points)

This round was a 10-10 tie. 

The fight is too close to call at this point.


Test B 0001 remains standing while the trainer wipes him down. He has swelling around his right eye. He has a cut below his right eye.

ZZTest B 0001 remains standing while the trainer wipes him down.

Test B 0001:

Your fighter lost 14.1 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 12.1 points of stun damage this round. He has accumulated 28 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 12.7 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 12.7 points of stun damage this round. He has accumulated 28 points of damage in the fight. ";



        const string _fullThing = @"

In this corner, standing 5 feet and 6 inches (168 centimeters) tall weighing in at 137 pounds (62 kilograms) with a record of 0 wins, 0 draws, and 0 losses is Test B 0001!!

In this corner, standing 5 feet and 6 inches (168 centimeters) tall weighing in at 137 pounds (62 kilograms) with a record of 0 wins, 0 draws, and 0 losses is ZZTest B 0001!!

This bout is scheduled for 12 rounds.


ROUND 1


Test B 0001 has 140.0 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.



ZZTest B 0001 has 140.0 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.


Test B 0001 comes out fighting and jabs for the cut.
ZZTest B 0001 comes out fighting and jabs for the cut.

...
...
ZZTest B 0001 lashes out with an uppercut to the nose, but Test B 0001 covers up .
ZZTest B 0001 lands a quick right hand to the nose. 
Test B 0001 lands a hook to the mouth. 
...
...
...
ZZTest B 0001 attacks with an uppercut to the eye, but Test B 0001 evades it .
...
...
ZZTest B 0001 lunges with a right to the mouth, but fails to score .
Test B 0001 attempts an uppercut to the face, but misses completely .
...
...
Test B 0001 connects with an uppercut to the mouth. 
Test B 0001 throws a cross to the nose, but falls short .
...
...
Test B 0001 hits with a right to the solar plexus. 
ZZTest B 0001 scores with a sharp hook to the eye. 
Test B 0001 pops ZZTest B 0001 with an excellent uppercut to the face. ZZTest B 0001 hangs on until the referee breaks them up.
...
Test B 0001 launches an uppercut to the face, but ZZTest B 0001 evades it .
...
ZZTest B 0001 lands an overhand right to the eye. 
ZZTest B 0001 connects with a hook to the mouth. 
...
...
...
...
...
Test B 0001 tries a left to the face, but ZZTest B 0001 ducks .
ZZTest B 0001 probes with an uppercut to the eye, but Test B 0001 ducks .
Test B 0001 connects with a hook to the eye. 
...
Test B 0001 punches ZZTest B 0001 with a sharp uppercut to the eye. ZZTest B 0001 covers up.
...
Test B 0001 annoys ZZTest B 0001 with a quick cross to the nose. 
ZZTest B 0001 hits with an overhand right to the stomach. 
ZZTest B 0001 hits Test B 0001 with a right to the nose. Test B 0001 quickly recovers.
...
Test B 0001 launches a hook to the face, but it's short .
...
ZZTest B 0001 takes charge with a good uppercut to the mouth. 

BELL!

According to the Commentators: 

Test B 0001 landed 27 of 45 punches -- 18 power punches, 0 jabs, 9 rights. (99 points)
ZZTest B 0001 landed 26 of 43 punches -- 17 power punches, 0 jabs, 9 rights. (95 points)

Test B 0001 won the round 10-9 . It was a close round, but Test B 0001 was more accurate.

Test B 0001 is winning the fight 10-9. 


Test B 0001 doesn't need to rest.

ZZTest B 0001 doesn't need to rest. He has a bloody mouth.

Test B 0001:

Your fighter lost 13.0 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 13.0 points of stun damage this round. He has accumulated 14 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 13.7 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 13.7 points of stun damage this round. He has accumulated 15 points of damage in the fight. 


ROUND 2


Test B 0001 has 125.6 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.



ZZTest B 0001 has 125.0 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.


Test B 0001 comes out fighting and jabs for the cut.
ZZTest B 0001 comes out fighting and jabs for the cut.

Test B 0001 tries a right to the eye, but ZZTest B 0001 ducks .
ZZTest B 0001 belts Test B 0001 with a heavy hook to the eye! 
Test B 0001 reaches with a cross to the eye, but ZZTest B 0001 ducks .
Test B 0001 annoys ZZTest B 0001 with a hook to the face. 
...
...
...
Test B 0001 hits with an uppercut to the face. 
ZZTest B 0001 throws a hook to the face, but Test B 0001 ducks .
ZZTest B 0001 lands a cross to the face. 
...
ZZTest B 0001 hits Test B 0001 with an uppercut to the nose. 
...
...
...
...
Test B 0001 lunges with an uppercut to the eye, but is ineffective .
ZZTest B 0001 launches an uppercut to the eye, but Test B 0001 blocks it .
ZZTest B 0001 lunges with a straight right to the nose, but Test B 0001 evades it .
...
Test B 0001 throws an uppercut to the ribs, but fails to score .
...
Test B 0001 fires a cross to the mouth. 
ZZTest B 0001 lands a right to the face. Test B 0001 sneers!
ZZTest B 0001 lashes out with a hook to the face, but Test B 0001 covers himself well .
Test B 0001 throws a quick left to the mouth. 
...
...
ZZTest B 0001 throws a roundhouse to the face. 
ZZTest B 0001 hits with an uppercut to the stomach. 
...
...
Test B 0001 annoys ZZTest B 0001 with a hook to the eye. 
...
...
...
...
...
...
Test B 0001 charges with a roundhouse to the eye, but flails uselessly .
Test B 0001 lashes out with a solid hook to the eye. ZZTest B 0001 hangs on until the referee breaks them up.
...
...
...
...

BELL!

According to the Commentators: 

Test B 0001 landed 24 of 43 punches -- 16 power punches, 0 jabs, 8 rights. (88 points)
ZZTest B 0001 landed 23 of 39 punches -- 15 power punches, 0 jabs, 8 rights. (84 points)

Test B 0001 won the round 10-9 with more accurate punching.

Test B 0001 is winning the fight 20-18. 


Test B 0001 remains standing while the trainer wipes him down. He has swelling around his right eye.

ZZTest B 0001 remains standing while the trainer wipes him down. He has swelling around his left eye. He has a cut below his left eye. He has a cut over his right eye. He has a bloody mouth.

Test B 0001:

Your fighter lost 11.9 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 11.9 points of stun damage this round. He has accumulated 28 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 16.2 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 12.2 points of stun damage this round. He has accumulated 29 points of damage in the fight. 


ROUND 3


Test B 0001 has 113.6 endurance points remaining.
Your tactics: aggressiveness = 1.0, power = 1.0, defense = 8.0, resting 10.0



ZZTest B 0001 has 109.2 endurance points remaining.
Your tactics: aggressiveness = 1.0, power = 1.0, defense = 8.0, resting 10.0


Test B 0001 appears to be resting .
ZZTest B 0001 appears to be resting .

...
...
...
ZZTest B 0001 attempts a straight right to the eye, but flails uselessly .
...
...
...
...
...
...
...
...
...
...
...
ZZTest B 0001 lands a cross to the temple. 
...
...
...
...
...
...
...
...
...
...
...
...
Test B 0001 lunges with an uppercut to the stomach, but can't connect .
...
...
...
Test B 0001 throws a quick straight right to the jaw. 
...
...
...
...
Test B 0001 throws a jab to the head, but ZZTest B 0001 blocks it .
...
...
...
...
...
...
...

BELL!

According to the Commentators: 

Test B 0001 landed 4 of 10 punches -- 1 power punch, 2 jabs, 1 right. (11 points)
ZZTest B 0001 landed 4 of 9 punches -- 1 power punch, 2 jabs, 1 right. (11 points)

This round was a 10-10 tie. 

Test B 0001 is winning the fight 30-28. 


Test B 0001 remains standing while the trainer wipes him down. He has swelling around his right eye.

ZZTest B 0001 remains standing while the trainer wipes him down. He has swelling around his left eye. He has a cut below his left eye. He has a cut over his right eye. He has a bloody mouth.

Test B 0001:

Your fighter lost 0.3 points of endurance this round due to damage, and 0.0 points due to fatigue. He took 0.3 points of stun damage this round. He has accumulated 28 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 0.4 points of endurance this round due to damage, and 0.0 points due to fatigue. He took 0.4 points of stun damage this round. He has accumulated 29 points of damage in the fight. 


ROUND 4


Test B 0001 has 121.3 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.



ZZTest B 0001 has 118.2 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.


Test B 0001 comes out fighting and jabs for the cut.
ZZTest B 0001 comes out fighting and jabs for the cut.

ZZTest B 0001 attempts an uppercut to the eye, but Test B 0001 blocks it .
ZZTest B 0001 tags Test B 0001 with a quick uppercut to the eye. 
Test B 0001 surprises ZZTest B 0001 with an excellent uppercut to the mouth. ZZTest B 0001 hangs on until the referee breaks them up.
...
...
...
...
...
ZZTest B 0001 scores with a left to the eye. 
Test B 0001 throws a hook to the eye. 
ZZTest B 0001 fires an uppercut to the mouth, but Test B 0001 slips it .
...
...
...
...
ZZTest B 0001 attacks with an overhand right to the face, but Test B 0001 covers himself well .
...
...
...
Test B 0001 annoys ZZTest B 0001 with a hook to the stomach. 
...
...
Test B 0001 lands a quick right to the mouth. ZZTest B 0001 ignores it.
ZZTest B 0001 launches a sweeping right to the stomach, but Test B 0001 evades it .
Test B 0001 connects with a roundhouse to the ribs. ZZTest B 0001 quickly recovers.
ZZTest B 0001 lashes out with a sweeping right to the eye, but Test B 0001 slips it .
ZZTest B 0001 hits Test B 0001 with a clean right to the nose. Test B 0001 ignores it.
...
Test B 0001 lashes out with a right hand to the face, but ZZTest B 0001 ducks .
...
ZZTest B 0001 hits Test B 0001 with an uppercut to the eye. 
ZZTest B 0001 punches Test B 0001 with a hook to the face. Test B 0001 covers up.
Test B 0001 lashes out with a left to the eye, but ZZTest B 0001 ducks .
...
...
...
Test B 0001 lashes out with an overhand right to the eye, but ZZTest B 0001 covers up .
...
ZZTest B 0001 tries to land an uppercut to the face, but Test B 0001 hides behind his gloves .
...
Test B 0001 charges with a hook to the eye, but he's off balance .
ZZTest B 0001 tries an uppercut to the face, but Test B 0001 covers himself well .
Test B 0001 punches ZZTest B 0001 with a right to the eye. 
...
Test B 0001 tries to land a hook to the face, but ZZTest B 0001 ducks .

BELL!

According to the Commentators: 

Test B 0001 landed 22 of 42 punches -- 15 power punches, 0 jabs, 7 rights. (81 points)
ZZTest B 0001 landed 21 of 49 punches -- 14 power punches, 0 jabs, 7 rights. (77 points)

Test B 0001 won the round 10-9 with more accurate punching.

Test B 0001 is winning the fight 40-37. 


Test B 0001 remains standing while the trainer wipes him down. He has swelling around his left eye. He has his right eye badly swollen.

ZZTest B 0001 remains standing while the trainer wipes him down. He has his left eye badly swollen. He has a cut below his left eye. He has a cut over his right eye. He has a bloody mouth.

Test B 0001:

Your fighter lost 11.3 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 11.3 points of stun damage this round. He has accumulated 41 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 13.9 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 11.9 points of stun damage this round. He has accumulated 42 points of damage in the fight. 


ROUND 5


Test B 0001 has 110.3 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.



ZZTest B 0001 has 105.2 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.


Test B 0001 comes out fighting and jabs for the cut.
ZZTest B 0001 comes out fighting and jabs for the cut.

...
ZZTest B 0001 lands an uppercut to the face. 
...
...
...
...
...
ZZTest B 0001 fires a roundhouse to the nose. 
Test B 0001 throws a hook to the mouth, but ZZTest B 0001 slips it .
ZZTest B 0001 lunges with a left to the eye, but doesn't quite connect .
ZZTest B 0001 launches a hook to the eye, but Test B 0001 covers himself well .
Test B 0001 lunges with a cross to the eye, but ZZTest B 0001 hides behind his gloves .
ZZTest B 0001 connects with a right to the stomach. 
...
...
...
Test B 0001 lands a quick left to the nose. 
ZZTest B 0001 lashes out with a hook to the eye, but Test B 0001 ducks .
...
Test B 0001 probes with an uppercut to the face, but it's soft .
...
ZZTest B 0001 tries to land a cross to the eye, but Test B 0001 covers up .
...
...
Test B 0001 tries to land a hook to the eye, but ZZTest B 0001 covers himself well .
ZZTest B 0001 attacks with a roundhouse to the ribs, but Test B 0001 blocks it .
...
...
Test B 0001 throws an uppercut to the face. ZZTest B 0001 ignores it.
ZZTest B 0001 hits with a right to the face. 
Test B 0001 smashes ZZTest B 0001 with a hard hook to the eye!! The crowd roars!! 
...
...
Test B 0001 attacks with a right to the nose, but ZZTest B 0001 covers himself well .
...
Test B 0001 probes with an overhand right to the mouth, but ZZTest B 0001 covers up .
...
Test B 0001 annoys ZZTest B 0001 with a right hand to the eye. 
ZZTest B 0001 throws a roundhouse to the face, but Test B 0001 ducks .
ZZTest B 0001 fires a clean roundhouse to the face. 
ZZTest B 0001 fires an uppercut to the nose, but can't connect .
Test B 0001 fires a hook to the eye. ZZTest B 0001 quickly recovers.
...
...
...

BELL!

According to the Commentators: 

Test B 0001 landed 21 of 44 punches -- 14 power punches, 0 jabs, 7 rights. (77 points)
ZZTest B 0001 landed 18 of 45 punches -- 12 power punches, 0 jabs, 6 rights. (66 points)

Test B 0001 won the round 10-9 with more accurate punching.

Test B 0001 is winning the fight 50-46. 


Test B 0001 grabs a water bottle and rests on his stool. He has his left eye badly swollen. He has his right eye badly swollen.

ZZTest B 0001 grabs a water bottle and rests on his stool. He has his left eye nearly swollen shut. He has a serious cut below his left eye. He has a cut over his right eye. He has a bloody mouth.

Test B 0001:

Your fighter lost 10.6 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 10.6 points of stun damage this round. He has accumulated 52 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 15.6 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 12.6 points of stun damage this round. He has accumulated 56 points of damage in the fight. 


ROUND 6


Test B 0001 has 101.0 endurance points remaining.
Your tactics: aggressiveness = 1.0, power = 1.0, defense = 8.0, resting 10.0



ZZTest B 0001 has 91.9 endurance points remaining.
Your tactics: aggressiveness = 1.0, power = 1.0, defense = 8.0, resting 10.0


Test B 0001 appears to be resting .
ZZTest B 0001 appears to be resting .

...
...
ZZTest B 0001 annoys Test B 0001 with a straight right to the stomach. 
...
...
Test B 0001 throws a quick right to the mouth. 
...
...
ZZTest B 0001 launches a jab to the jaw, but it's short .
...
...
...
...
ZZTest B 0001 attempts an uppercut to the stomach, but it's short .
...
...
...
Test B 0001 attacks with a sweeping right to the stomach, but ZZTest B 0001 slips it .
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...

BELL!

According to the Commentators: 

Test B 0001 landed 4 of 9 punches -- 1 power punch, 2 jabs, 1 right. (11 points)
ZZTest B 0001 landed 3 of 9 punches -- 1 power punch, 1 jab, 1 right. (9 points)

This round was a 10-10 tie. 

Test B 0001 is winning the fight 60-56. 


Test B 0001 grabs a water bottle and rests on his stool. He has his left eye badly swollen. He has his right eye badly swollen.

ZZTest B 0001 grabs a water bottle and rests on his stool. He has his left eye nearly swollen shut. He has a serious cut below his left eye. He has a cut over his right eye. He has a bloody mouth.

Test B 0001:

Your fighter lost 0.3 points of endurance this round due to damage, and 0.0 points due to fatigue. He took 0.3 points of stun damage this round. He has accumulated 53 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 0.4 points of endurance this round due to damage, and 0.0 points due to fatigue. He took 0.4 points of stun damage this round. He has accumulated 57 points of damage in the fight. 


ROUND 7


Test B 0001 has 112.5 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.



ZZTest B 0001 has 106.1 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.


Test B 0001 comes out fighting and jabs for the cut.
ZZTest B 0001 comes out fighting and jabs for the cut.

ZZTest B 0001 launches a straight right to the ribs, but Test B 0001 hides behind his gloves .
Test B 0001 throws a straight right to the eye, but ZZTest B 0001 slips it .
...
Test B 0001 wallops ZZTest B 0001 with a big hook to the mouth!! The crowd is on its feet!! ZZTest B 0001 grimaces in pain.
ZZTest B 0001 hits Test B 0001 with a clean uppercut to the eye. Test B 0001 ignores it.
Test B 0001 lashes out with an uppercut to the mouth, but can't connect .
Test B 0001 lunges with a hook to the mouth, but ZZTest B 0001 evades it .
...
...
ZZTest B 0001 reaches with an uppercut to the mouth, but Test B 0001 slips it .
...
ZZTest B 0001 probes with an uppercut to the eye, but Test B 0001 ducks .
ZZTest B 0001 tries an uppercut to the eye, but doesn't land it very well .
Test B 0001 lunges with an uppercut to the nose, but ZZTest B 0001 covers up .
Test B 0001 tries a left to the face, but it's too slow .
ZZTest B 0001 punches Test B 0001 with a hook to the nose. 
...
...
...
...
...
...
Test B 0001 attempts an overhand right to the chest, but ZZTest B 0001 hides behind his gloves .
Test B 0001 annoys ZZTest B 0001 with a quick uppercut to the mouth. 
...
...
ZZTest B 0001 hits with a hook to the eye. 
Test B 0001 annoys ZZTest B 0001 with a left to the face. 
ZZTest B 0001 lunges with an uppercut to the mouth, but misses completely .
...
ZZTest B 0001 tries to land a sweeping right to the nose, but Test B 0001 blocks it .
Test B 0001 throws an overhand right to the mouth. 
Test B 0001 throws a cross to the nose, but it's too slow .
...
ZZTest B 0001 connects with a quick right to the mouth. 
Test B 0001 probes with an uppercut to the eye, but goes wide .
ZZTest B 0001 charges with a cross to the nose, but misses completely .
ZZTest B 0001 probes with an overhand right to the face, but Test B 0001 evades it .
...
...
ZZTest B 0001 charges with an uppercut to the eye, but Test B 0001 covers up .
...
...
Test B 0001 scores with a sweeping right to the chest. 
...

BELL!

According to the Commentators: 

Test B 0001 landed 21 of 52 punches -- 14 power punches, 0 jabs, 7 rights. (77 points)
ZZTest B 0001 landed 15 of 49 punches -- 10 power punches, 0 jabs, 5 rights. (55 points)

Test B 0001 won the round 10-9 with more accurate punching.

Test B 0001 is winning the fight 70-65. 


Test B 0001 grabs a water bottle and rests on his stool. He has his left eye nearly swollen shut. He has his right eye nearly swollen shut.

ZZTest B 0001 is obviously tired. He has his left eye swollen shut. He has a serious cut below his left eye. He has a serious cut over his right eye. He has a bloody mouth.

Test B 0001:

Your fighter lost 9.8 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 9.8 points of stun damage this round. He has accumulated 64 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 19.2 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 13.2 points of stun damage this round. He has accumulated 71 points of damage in the fight. 


ROUND 8


Test B 0001 has 103.7 endurance points remaining.
Your tactics: aggressiveness = 1.0, power = 1.0, defense = 8.0, resting 10.0



ZZTest B 0001 has 89.5 endurance points remaining.
Your tactics: aggressiveness = 1.0, power = 1.0, defense = 8.0, resting 10.0


Test B 0001 appears to be resting .
ZZTest B 0001 appears to be resting .

...
Test B 0001 lashes out with a jab to the solar plexus, but is ineffective .
...
...
Test B 0001 fires a hook to the head, but ZZTest B 0001 evades it .
...
Test B 0001 connects with a straight right to the nose. 
...
...
...
...
...
...
...
...
...
...
...
...
...
...
...
ZZTest B 0001 attacks with a hook to the temple, but Test B 0001 ducks .
...
...
...
ZZTest B 0001 fires a jab to the face, but Test B 0001 ducks .
...
...
...
...
...
...
...
...
ZZTest B 0001 annoys Test B 0001 with a sweeping right to the chest. 
...
...
...
...
...
...
...
...
...

BELL!

According to the Commentators: 

Test B 0001 landed 3 of 9 punches -- 1 power punch, 1 jab, 1 right. (9 points)
ZZTest B 0001 landed 0 of 9 punches -- 0 power punches, 0 jabs, 0 right. (0 points)

Test B 0001 won the round 10-9 with more accurate punching.

Test B 0001 is winning the fight 80-74. 


Test B 0001 remains standing while the trainer wipes him down. He has his left eye nearly swollen shut. He has his right eye nearly swollen shut.

ZZTest B 0001 grabs a water bottle and rests on his stool. He has his left eye swollen shut. He has a serious cut below his left eye. He has a serious cut over his right eye. He has a bloody mouth.

Test B 0001:

Your fighter lost 0.2 points of endurance this round due to damage, and 0.0 points due to fatigue. He took 0.2 points of stun damage this round. He has accumulated 64 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 0.7 points of endurance this round due to damage, and 0.0 points due to fatigue. He took 0.7 points of stun damage this round. He has accumulated 72 points of damage in the fight. 


ROUND 9


Test B 0001 has 114.4 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.



ZZTest B 0001 has 104.2 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.


Test B 0001 comes out fighting and jabs for the cut.
ZZTest B 0001 comes out fighting and jabs for the cut.

...
Test B 0001 charges with an uppercut to the face, but doesn't land it very well .
...
...
ZZTest B 0001 probes with a hook to the eye, but Test B 0001 blocks it .
Test B 0001 throws a roundhouse to the eye, but ZZTest B 0001 hides behind his gloves .
...
...
...
...
...
ZZTest B 0001 reaches with a right hand to the eye, but Test B 0001 blocks it .
ZZTest B 0001 reaches with an uppercut to the solar plexus, but Test B 0001 blocks it .
Test B 0001 jars ZZTest B 0001 with a big hook to the face!! The crowd is on its feet!! 
Test B 0001 tries a cross to the face, but ZZTest B 0001 covers himself well .
Test B 0001 throws a quick right hand to the mouth. 

Test B 0001 lands a vicious left blow to the temple and ZZTest B 0001 stumbles to the canvass!! 

ONE! 

TWO! 

THREE! 

FOUR! 

FIVE! 

SIX! 

SEVEN! 

ZZTest B 0001 climbs to his feet and the fight goes on!

ZZTest B 0001 throws an uppercut to the nose, but Test B 0001 ducks .
...
Test B 0001 reaches with a cross to the eye, but he's off balance .
ZZTest B 0001 throws a cross to the nose, but only touches with it .
...
...
Test B 0001 throws a left to the eye. 
...
...
...
Test B 0001 hits with an uppercut to the eye. 
...
...
Test B 0001 probes with an uppercut to the eye, but ZZTest B 0001 blocks it .
...
ZZTest B 0001 charges with a right to the eye, but doesn't land it very well .
Test B 0001 slams ZZTest B 0001 with a mean right to the eye! 
...
ZZTest B 0001 nails Test B 0001 with a furious uppercut to the eye. 
...
...
...
ZZTest B 0001 tries a roundhouse to the eye, but is ineffective .
Test B 0001 tries a roundhouse to the eye, but he's off balance .
...
...
...
ZZTest B 0001 tries to land an uppercut to the mouth, but Test B 0001 blocks it .
...

BELL!

According to the Commentators: 

Test B 0001 landed 20 of 43 punches -- 14 power punches, 0 jabs, 6 rights. (74 points)
ZZTest B 0001 landed 3 of 34 punches -- 2 power punches, 0 jabs, 1 right. (11 points)

Test B 0001 won the round 10-8 for the knockdown.

Test B 0001 is winning the fight 90-82. 


Test B 0001 remains standing while the trainer wipes him down. He has his left eye nearly swollen shut. He has his right eye swollen shut.

ZZTest B 0001 is obviously tired. He has his left eye swollen shut. He has a gash below his left eye. He has a cut below his right eye. He has bleeding over his left eye. He has a serious cut over his right eye. He has a broken tooth.

Test B 0001:

Your fighter lost 6.2 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 6.2 points of stun damage this round. He has accumulated 71 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 27.7 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 20.7 points of stun damage this round. He has accumulated 95 points of damage in the fight. 


ROUND 10


Test B 0001 has 108.7 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.



ZZTest B 0001 has 80.1 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.


Test B 0001 comes out fighting and jabs for the cut.
ZZTest B 0001 comes out fighting and jabs for the cut.

Test B 0001 throws an overhand right to the eye, but ZZTest B 0001 evades it .
...
ZZTest B 0001 throws a hook to the face, but Test B 0001 ducks .
Test B 0001 jars ZZTest B 0001 with a big roundhouse to the mouth!! The crowd is on its feet!! 
Test B 0001 hits with a cross to the mouth. 
Test B 0001 jars ZZTest B 0001 with a big hook to the eye!! The crowd is on its feet!! 
...
Test B 0001 charges with a right to the nose, but ZZTest B 0001 covers himself well .
ZZTest B 0001 attempts a left to the nose, but doesn't quite connect .
Test B 0001 tries to land a right hand to the eye, but comes up empty .
ZZTest B 0001 fires a hook to the eye, but goes wide .
...
Test B 0001 reaches with an overhand right to the nose, but flails uselessly .
Test B 0001 clocks ZZTest B 0001 with a wild uppercut to the nose. 
...
...
...
...
ZZTest B 0001 punches Test B 0001 with a hook to the mouth. 
ZZTest B 0001 attacks with an uppercut to the face, but it's short .
...
Test B 0001 attempts a hook to the face, but doesn't quite connect .
...
...
Test B 0001 probes with an uppercut to the mouth, but ZZTest B 0001 blocks it .
Test B 0001 launches a hook to the mouth, but ZZTest B 0001 blocks it .
Test B 0001 lunges with a cross to the face, but ZZTest B 0001 ducks .
...
ZZTest B 0001 throws an uppercut to the face, but Test B 0001 hides behind his gloves .
ZZTest B 0001 charges with a hook to the face, but goes wide .
...
...
...
ZZTest B 0001 attempts a left to the eye, but Test B 0001 covers himself well .
...
...
...
...
...
...
...

...
...

Test B 0001 lands a mighty bomb and ZZTest B 0001 is lifted off his feet onto his back!! 

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
Test B 0001 wins by a Knock Out!!

Time: 2:42

Test B 0001 remains standing while the trainer wipes him down. He has his left eye swollen shut. He has his right eye swollen shut.

ZZTest B 0001 collapses limply onto his stool. He has his left eye swollen shut. He has swelling around his right eye. He has a gash below his left eye. He has a cut below his right eye. He has bleeding over his left eye. He has a serious cut over his right eye. He has a broken tooth.

Test B 0001:

Your fighter lost 3.7 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 3.7 points of stun damage this round. He has accumulated 75 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 41.7 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 27.7 points of stun damage this round. He has accumulated 126 points of damage in the fight. 






Judge Roy Bean had the fight scored as follows: 

Round 1: Test B 0001 10-9
Round 2: Test B 0001 10-9
Round 3: A 10-10 tie.
Round 4: A 10-10 tie.
Round 5: Test B 0001 10-9
Round 6: A 10-10 tie.
Round 7: Test B 0001 10-9
Round 8: Test B 0001 10-9
Round 9: Test B 0001 10-8



Judge Judy had the fight scored as follows: 

Round 1: ZZTest B 0001 10-9
Round 2: Test B 0001 10-9
Round 3: A 10-10 tie.
Round 4: Test B 0001 10-9
Round 5: Test B 0001 10-9
Round 6: A 10-10 tie.
Round 7: Test B 0001 10-9
Round 8: Test B 0001 10-9
Round 9: Test B 0001 10-8



Judge Lao Mang Chen had the fight scored as follows: 

Round 1: Test B 0001 10-9
Round 2: Test B 0001 10-9
Round 3: A 10-10 tie.
Round 4: ZZTest B 0001 10-9
Round 5: Test B 0001 10-9
Round 6: A 10-10 tie.
Round 7: Test B 0001 10-9
Round 8: Test B 0001 10-9
Round 9: Test B 0001 10-8


";

        const string _koRound =
@"ROUND 10


Test B 0001 has 108.7 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.



ZZTest B 0001 has 80.1 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.


Test B 0001 comes out fighting and jabs for the cut.
ZZTest B 0001 comes out fighting and jabs for the cut.

Test B 0001 throws an overhand right to the eye, but ZZTest B 0001 evades it .
...
ZZTest B 0001 throws a hook to the face, but Test B 0001 ducks.
Test B 0001 jars ZZTest B 0001 with a big roundhouse to the mouth!! The crowd is on its feet!! 
Test B 0001 hits with a cross to the mouth.
Test B 0001 jars ZZTest B 0001 with a big hook to the eye!! The crowd is on its feet!! 
...
Test B 0001 charges with a right to the nose, but ZZTest B 0001 covers himself well.
ZZTest B 0001 attempts a left to the nose, but doesn't quite connect .
Test B 0001 tries to land a right hand to the eye, but comes up empty .
ZZTest B 0001 fires a hook to the eye, but goes wide.
...
Test B 0001 reaches with an overhand right to the nose, but flails uselessly.
Test B 0001 clocks ZZTest B 0001 with a wild uppercut to the nose.
...
...
...
...
ZZTest B 0001 punches Test B 0001 with a hook to the mouth. 
ZZTest B 0001 attacks with an uppercut to the face, but it's short .
...
Test B 0001 attempts a hook to the face, but doesn't quite connect .
...
...
Test B 0001 probes with an uppercut to the mouth, but ZZTest B 0001 blocks it .
Test B 0001 launches a hook to the mouth, but ZZTest B 0001 blocks it .
Test B 0001 lunges with a cross to the face, but ZZTest B 0001 ducks.
...
ZZTest B 0001 throws an uppercut to the face, but Test B 0001 hides behind his gloves .
ZZTest B 0001 charges with a hook to the face, but goes wide.
...
...
...
ZZTest B 0001 attempts a left to the eye, but Test B 0001 covers himself well.
...
...
...
...
...
...
...

...
...

Test B 0001 lands a mighty bomb and ZZTest B 0001 is lifted off his feet onto his back!! 

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
Test B 0001 wins by a Knock Out!!

Time: 2:42

Test B 0001 remains standing while the trainer wipes him down.He has his left eye swollen shut.He has his right eye swollen shut.

ZZTest B 0001 collapses limply onto his stool.He has his left eye swollen shut.He has swelling around his right eye.He has a gash below his left eye. He has a cut below his right eye. He has bleeding over his left eye.He has a serious cut over his right eye.He has a broken tooth.

Test B 0001:

Your fighter lost 3.7 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 3.7 points of stun damage this round. He has accumulated 75 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 41.7 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 27.7 points of stun damage this round. He has accumulated 126 points of damage in the fight. 






Judge Roy Bean had the fight scored as follows: 

Round 1: Test B 0001 10-9
Round 2: Test B 0001 10-9
Round 3: A 10-10 tie.
Round 4: A 10-10 tie.
Round 5: Test B 0001 10-9
Round 6: A 10-10 tie.
Round 7: Test B 0001 10-9
Round 8: Test B 0001 10-9
Round 9: Test B 0001 10-8



Judge Judy had the fight scored as follows: 

Round 1: ZZTest B 0001 10-9
Round 2: Test B 0001 10-9
Round 3: A 10-10 tie.
Round 4: Test B 0001 10-9
Round 5: Test B 0001 10-9
Round 6: A 10-10 tie.
Round 7: Test B 0001 10-9
Round 8: Test B 0001 10-9
Round 9: Test B 0001 10-8



Judge Lao Mang Chen had the fight scored as follows: 

Round 1: Test B 0001 10-9
Round 2: Test B 0001 10-9
Round 3: A 10-10 tie.
Round 4: ZZTest B 0001 10-9
Round 5: Test B 0001 10-9
Round 6: A 10-10 tie.
Round 7: Test B 0001 10-9
Round 8: Test B 0001 10-9
Round 9: Test B 0001 10-8


";

        const string _tkoRound = @"ROUND 9


Test B 0001 has 108.7 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.



ZZTest B 0001 has 114.0 endurance points remaining.
Your tactics: aggressiveness = 5.0, power = 10.0, defense = 5.0, resting 0.0
You are aiming at his injuries.


Test B 0001 comes out fighting and jabs for the cut.
ZZTest B 0001 comes out fighting and jabs for the cut.

...
ZZTest B 0001 hits with a hook to the nose. 
...
ZZTest B 0001 hits Test B 0001 with an uppercut to the eye. Test B 0001 quickly recovers.
...
ZZTest B 0001 attempts an overhand right to the eye, but it's short .
...
ZZTest B 0001 throws an overhand right to the eye, but comes up empty .
ZZTest B 0001 charges with an overhand right to the stomach, but fails to score .
Test B 0001 probes with a hook to the nose, but ZZTest B 0001 covers himself well .
...
ZZTest B 0001 tries a cross to the nose, but Test B 0001 ducks .
Test B 0001 fires an uppercut to the nose, but ZZTest B 0001 evades it .
ZZTest B 0001 throws a right to the eye. 
...
...
...
ZZTest B 0001 throws a straight right to the mouth. 

ZZTest B 0001 lands a sudden right haymaker and Test B 0001 falls senseless!! 

ONE! 

TWO! 

THREE! 

FOUR! 

FIVE! 

SIX! 

Test B 0001 gets up and the fight goes on!

...
...
...
ZZTest B 0001 probes with an uppercut to the nose, but comes up empty .
ZZTest B 0001 jars Test B 0001 with a big uppercut to the eye!! The crowd is on its feet!! 
Test B 0001 throws a roundhouse to the eye, but ZZTest B 0001 evades it .
ZZTest B 0001 hits with a clean hook to the mouth. 
Test B 0001 catches ZZTest B 0001 with an excellent hook to the nose. 
Test B 0001 fires a sweeping right to the face, but ZZTest B 0001 covers himself well .
...
Test B 0001 reaches with a left to the nose, but falls short .
...
...
...
Test B 0001 fires an uppercut to the face, but fails to score .
...
...
...
ZZTest B 0001 reaches with a left to the eye, but it's short .
ZZTest B 0001 lunges with a roundhouse to the eye, but can't connect .
...
Test B 0001 lashes out with a hook to the eye, but ZZTest B 0001 hides behind his gloves .
...
ZZTest B 0001 lands a right hand to the mouth. 
ZZTest B 0001 fires an overhand right to the nose. Test B 0001 quickly recovers.
...
Test B 0001 tries to land a sweeping right to the nose, but ZZTest B 0001 ducks .
Test B 0001 reaches with a hook to the face, but ZZTest B 0001 ducks .
...
...
...
...
...


Time: 3:00

Test B 0001 is obviously tired. He has his left eye swollen shut. He has his right eye swollen shut. He has a serious cut over his left eye. He has a fractured nose. He has a bloody lip.

ZZTest B 0001 remains standing while the trainer wipes him down. He has swelling around his left eye. He has a cut below his left eye.

Test B 0001:

Your fighter lost 29.6 points of endurance this round due to damage, and 4.0 points due to fatigue. He took 20.6 points of stun damage this round. He has accumulated 95 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 7.2 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 5.2 points of stun damage this round. He has accumulated 71 points of damage in the fight. 
The Doctor won't let Test B 0001 continue the fight because of condition of his eyes! ZZTest B 0001 wins by a TKO!



";

        const string _dqRound = @"ROUND 7


Test 23523 has 108.1 endurance points remaining.
Your tactics: aggressiveness = 6.0, power = 6.0, defense = 8.0, resting 0.0



The Sparring Partner has 76.9 endurance points remaining.
Your tactics: aggressiveness = 4.0, power = 8.0, defense = 8.0, resting 0.0


23523 comes out fighting.
Partner comes out fighting.

23523 reaches with a straight right to the jaw, but doesn't land it very well .
...
...
...
...
23523 throws a jab to the jaw. 
Partner launches a left to the eye, but 23523 blocks it .
...
...
Partner annoys 23523 with a quick hook to the solar plexus. 
23523 tries a straight right to the face, but Partner evades it .
23523 lashes out with an uppercut to the head, but Partner covers himself well .
23523 fires a hook to the head. 
Partner charges with an uppercut to the chin, but 23523 hides behind his gloves .
...
...
23523 tags Partner with a jab to the temple. 
Partner throws a roundhouse to the stomach, but fails to score .
...
Partner charges with an overhand right to the ribs, but 23523 evades it .
Partner lands a left to the stomach. 
Partner punches 23523 with a hook to the jaw. 
23523 throws a sweeping right to the jaw, but Partner covers up .
Partner throws a straight right to the head, but 23523 covers up .
...
...
...
...
...
23523 lunges with a right hand to the eye, but it's weak .
...
23523 fires a jab to the ribs, but doesn't quite connect .
23523 connects with a quick right hand to the head. Partner ignores it.
23523 attacks with an uppercut to the stomach, but Partner covers up .
23523 launches a jab to the stomach, but it's soft .
...
23523 attacks with a hook to the ribs, but Partner slips it .
...
...
23523 tries to land a jab to the temple, but it's too slow .
23523 throws an uppercut to the ribs. 

Partner hammers 23523 with an illegal rabbit punch! Test 23523 goes to his corner in protest! The referee disqualifies Partner!
Test 23523 wins by DQ!!
Time: 2:45

23523 remains standing while the trainer wipes him down.

Partner grabs a water bottle and rests on his stool.

Test 23523:

Your fighter lost 4.9 points of endurance this round due to damage, and 2.0 points due to fatigue. He took 9.1 points of stun damage this round. He has accumulated 39 points of damage in the fight.

The Sparring Partner:

Your fighter lost 5.3 points of endurance this round due to damage, and 2.5 points due to fatigue. He took 4.5 points of stun damage this round. He has accumulated 37 points of damage in the fight. 






Judge Roy Bean had the fight scored as follows: 

Round 1: Test 23523 10-9
Round 2: Test 23523 10-9";


        public const string _judgeString = @"9.7 points of endurance this round due to damage, and 3.0 points due to fatigue. He took 8.7 points of stun damage this round. He has accumulated 72 points of damage in the fight. 
The Doctor won't let Test B 0001 continue the fight because of a gash over his right eye! ZZTest B 0001 wins by a TKO!






Judge Roy Bean had the fight scored as follows: 

Round 1: A 10-10 tie.
Round 2: A 10-10 tie.
Round 3: A 10-10 tie.
Round 4: ZZTest B 0001 10-9
Round 5: ZZTest B 0001 10-9
Round 6: A 10-10 tie.
Round 7: ZZTest B 0001 10-9
Round 8: A 10-10 tie.



Judge Judy had the fight scored as follows: 

Round 1: A 10-10 tie.
Round 2: A 10-10 tie.
Round 3: A 10-10 tie.
Round 4: ZZTest B 0001 10-9
Round 5: ZZTest B 0001 10-9
Round 6: A 10-10 tie.
Round 7: ZZTest B 0001 10-9
Round 8: A 10-10 tie.



Judge Lao Mang Chen had the fight scored as follows: 

Round 1: A 10-10 tie.
Round 2: A 10-10 tie.
Round 3: A 10-10 tie.
Round 4: ZZTest B 0001 10-9
Round 5: ZZTest B 0001 10-9
Round 6: A 10-10 tie.
Round 7: ZZTest B 0001 10-9
Round 8: A 10-10 tie.



";

        const string _clinchRound = @"
ROUND 1


Test B 0001 has 140.0 endurance points remaining.
Your tactics: aggressiveness = 3.4, power = 8.0, defense = 8.0, resting 0.6
Your AGG is reduced by clinching. You are trying to wear him out with body blows.



ZZTest B 0001 has 140.0 endurance points remaining.
Your tactics: aggressiveness = 3.4, power = 8.0, defense = 8.0, resting 0.6
Your AGG is reduced by clinching. You are trying to wear him out with body blows.


Test B 0001 is clinching a lot (clinching) and goes to the body.
ZZTest B 0001 is clinching a lot (clinching) and goes to the body.

ZZTest B 0001 lands a roundhouse to the stomach. 
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
Test B 0001 charges with a hook to the mouth, but falls short .
Test B 0001 tags ZZTest B 0001 with a clean cross to the solar plexus. 
Test B 0001 tries a right to the stomach, but ZZTest B 0001 falls into a clinch .
ZZTest B 0001 attempts an overhand right to the stomach, but Test B 0001 hangs on until the referee breaks them up .
...
ZZTest B 0001 tries to attack, but Test B 0001 hangs on until the referee separates them.
Test B 0001 fires a hook to the stomach, but ZZTest B 0001 ties him up .
ZZTest B 0001 scores with an overhand right to the stomach. 
...
ZZTest B 0001 tries a straight right to the stomach, but Test B 0001 ties him up .
...
...
Test B 0001 probes with an uppercut to the chest, but ZZTest B 0001 falls into a clinch .
Test B 0001 throws a hook to the head, but it's too slow .
...
ZZTest B 0001 reaches with an overhand right to the jaw, but it's short .
...
...
ZZTest B 0001 probes with an uppercut to the chest, but it's too slow .
...
ZZTest B 0001 lands a clean right to the stomach. Test B 0001 ignores it.
...
...
The referee admonishes Test B 0001 to stop clinching.
Test B 0001 lands a quick roundhouse to the ribs. ZZTest B 0001 sneers!
...
...
Test B 0001 tries to close, but ZZTest B 0001 ties him up.
...
ZZTest B 0001 attacks with an uppercut to the ribs, but Test B 0001 hangs on until the referee breaks them up .
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
...
ZZTest B 0001 lunges with a cross to the ribs, but Test B 0001 falls into a clinch .
...
...
Test B 0001 probes with an uppercut to the chin, but ZZTest B 0001 falls into a clinch .
Test B 0001 tries to attack, but ZZTest B 0001 hangs on until the referee separates them.
...
...
ZZTest B 0001 tries to attack, but Test B 0001 hangs on until the referee separates them.
...
...
...
ZZTest B 0001 lunges with a hook to the stomach, but it's soft .
...
ZZTest B 0001 tries to attack, but Test B 0001 hangs on until the referee separates them.
...
...
Test B 0001 fires a sharp overhand right to the stomach. 
...
...

BELL!

According to the Commentators: 

Test B 0001 landed 10 of 33 punches -- 8 power punches, 0 jabs, 2 rights. (38 points)
ZZTest B 0001 landed 10 of 36 punches -- 8 power punches, 0 jabs, 2 rights. (38 points)

This round was a 10-10 tie. 

The fight is too close to call at this point.


Test B 0001 doesn't need to rest.

ZZTest B 0001 doesn't need to rest.

Test B 0001:

Your fighter lost 6.6 points of endurance this round due to damage, and 0.4 points due to fatigue. He took 4.4 points of stun damage this round. He has accumulated 6 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 6.6 points of endurance this round due to damage, and 0.4 points due to fatigue. He took 4.4 points of stun damage this round. He has accumulated 6 points of damage in the fight. 


";
    }
}