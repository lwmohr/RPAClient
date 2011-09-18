using RockPaperScissorsPro;
using System;

namespace RockPaperAzure
{
    public class MyBot : IRockPaperScissorsBot
    {
        // Random sample implementation
        //public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        //{
        //     return Moves.GetRandomMove();
        //}
        int tiesCounter = 0;

        bool firstTime = true;
        bool twoTieWater = false;
        int dynaTwice, dynaSingleWater, dynaTwiceWater, waterSingle;


        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            if (firstTime)
            {
                dynaTwice = Moves.GetRandomNumber(500);
                dynaSingleWater = Moves.GetRandomNumber(500);
                dynaTwiceWater = Moves.GetRandomNumber(500);
                //dynaTwiceWater = 5;
                waterSingle = Moves.GetRandomNumber(500);
                firstTime = false;
            }

            //float highPoint = you.Points;
            //float totalThrows = you.NumberOfDecisions;
            //float remainingDynamite = you.DynamiteRemaining;
            //if (you.Points < opponent.Points)
            //    highPoint = opponent.Points;
            //float winPercent = highPoint / totalThrows;
            //float predictedThrows = (float)1000 / winPercent;
            //float remainingThrows = predictedThrows - totalThrows;
            //float oddsForDynamite = remainingDynamite / remainingThrows;

            if ((dynaTwice == you.NumberOfDecisions) || ((dynaTwice + 1) == you.NumberOfDecisions))
                return Moves.Dynamite;
            if (dynaSingleWater == you.NumberOfDecisions)
                return Moves.Dynamite;
            if ((dynaSingleWater + 1) == you.NumberOfDecisions)
                return Moves.WaterBalloon;
            if ((dynaTwiceWater == you.NumberOfDecisions) || ((dynaTwiceWater + 1) == you.NumberOfDecisions))
                return Moves.Dynamite;
            if ((dynaTwiceWater + 2) == you.NumberOfDecisions)
                return Moves.WaterBalloon;
            if (waterSingle == you.NumberOfDecisions)
                return Moves.WaterBalloon;

            if (tiesCounter >= 2 && opponent.LastMove == Moves.WaterBalloon)
                twoTieWater = true;

            if (you.LastMove == opponent.LastMove)
                tiesCounter++;
            else
                tiesCounter = 0;

            if (you.HasDynamite)
            {
                if (tiesCounter > 0)
                {
                    int myRand = Moves.GetRandomNumber(100);
                    switch (tiesCounter){

                        case 1:
                            if (myRand < 4)
                                return Moves.Dynamite;
                            break;
                        case 2:
                            if (myRand < 20)
                                return Moves.Dynamite;
                            break;
                        default:
                            if (!twoTieWater)
                                return Moves.Dynamite;
                            else if (myRand < 50)
                                return Moves.Dynamite;
                            break;
                    }
                }
                if (you.DynamiteRemaining >= Moves.GetRandomNumber((1000 - opponent.Points) * 4))
                    return Moves.Dynamite;
            }
            return Moves.GetRandomMove();
        }

        // Cycle sample implementation
        //public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        //{
        //    if (you.LastMove == Moves.Rock)
        //        return Moves.Paper;

        //    if (you.LastMove == Moves.Paper)
        //        return Moves.Scissors;

        //    if (you.LastMove == Moves.Scissors)
        //        if (you.HasDynamite)
        //            return Moves.Dynamite;
        //        else
        //            return Moves.WaterBalloon;

        //    if (you.LastMove == Moves.Dynamite)
        //        return Moves.WaterBalloon;

        //    return Moves.Rock;
        //}

        // BigBang sample implementation
        //public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        //{
        //    if (you.NumberOfDecisions < 5)
        //        return Moves.Dynamite;
        //    else
        //        return Moves.GetRandomMove();
        //}
        //public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        //{
        //    float highPoint = you.Points;
        //    float totalThrows = you.NumberOfDecisions;
        //    float remainingDynamite = you.DynamiteRemaining;
        //    if (you.Points < opponent.Points)
        //        highPoint = opponent.Points;
        //    float winPercent = highPoint / totalThrows;
        //    float predictedThrows = 1000 / winPercent;
        //    float remainingThrows = predictedThrows - totalThrows;
        //    float oddsForDynamite = remainingDynamite / remainingThrows;


        //    if (opponent.LastMove == Moves.Dynamite)
        //        hisDynamite--;
        //    if (you.LastMove == Moves.Dynamite)
        //        myDynamite--;
        //    if (you.HasDynamite && opponent.HasDynamite)
        //    {
        //        int myRand = Moves.GetRandomNumber(19);
        //        if (myRand == 18)
        //            return Moves.Dynamite;
        //        return Moves.GetRandomMove();
        //    }
        //    if (you.HasDynamite && !opponent.HasDynamite)
        //    {
        //        int myRand = Moves.GetRandomNumber(5);
        //        if (myRand == 3)
        //            return Moves.Dynamite;
        //        return Moves.GetRandomMove();
        //    }
        //    return Moves.GetRandomMove();
        //}
    }
}
