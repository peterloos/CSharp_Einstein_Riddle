using System;
using System.Diagnostics;

class EinsteinRiddleSolver
{
    private House[] houses;
    private char[][] four_permutations;
    private int[][] five_permutations;

    // c'tor
    public EinsteinRiddleSolver()
    {
        this.InitPermutations();

        // create five houses
        this.houses = new House[5];
        for (int i = 0; i < this.houses.Length; i++)
            this.houses[i] = new House();

        // Tipp 7: "Der Mann, der im mittleren Haus wohnt, trinkt gerne Milch"
        this.houses[2].Drink = Drink.Milk;

        // Tipp 9: "Der Norweger wohnt im ersten Haus"
        this.houses[0].Nationality = Nationality.Norwegian;

        // Tipp 13: "Neben dem blauen Haus wohnt der Norweger"
        this.houses[1].HouseColor = HouseColor.Blue;
    }

    private void InitPermutations()
    {
        this.four_permutations = new char[][]
        {
            new char[] {'a','b','c','d'}, new char[] {'a','c','b','d'},
            new char[] {'b','a','c','d'}, new char[] {'b','c','a','d'},
            new char[] {'c','a','b','d'}, new char[] {'c','b','a','d'},
            new char[] {'a','b','d','c'}, new char[] {'a','c','d','b'},
            new char[] {'b','a','d','c'}, new char[] {'b','c','d','a'},
            new char[] {'c','a','d','b'}, new char[] {'c','b','d','a'},
            new char[] {'a','d','b','c'}, new char[] {'a','d','c','b'},
            new char[] {'b','d','a','c'}, new char[] {'b','d','c','a'},
            new char[] {'c','d','a','b'}, new char[] {'c','d','b','a'},
            new char[] {'d','a','b','c'}, new char[] {'d','a','c','b'},
            new char[] {'d','b','a','c'}, new char[] {'d','b','c','a'},
            new char[] {'d','c','a','b'}, new char[] {'d','c','b','a'}
        };

        this.five_permutations = new int[][]
        {
             new int[] { 0,1,2,3,4 }, new int[] { 0,1,2,4,3 },
             new int[] { 0,1,3,2,4 }, new int[] { 0,1,3,4,2 },
             new int[] { 0,1,4,2,3 }, new int[] { 0,1,4,3,2 },
             new int[] { 0,2,1,3,4 }, new int[] { 0,2,1,4,3 }, new int[] { 0,2,3,1,4 },
             new int[] { 0,2,3,4,1 }, new int[] { 0,2,4,1,3 }, new int[] { 0,2,4,3,1 },
             new int[] { 0,3,1,2,4 }, new int[] { 0,3,1,4,2 }, new int[] { 0,3,2,1,4 },
             new int[] { 0,3,2,4,1 }, new int[] { 0,3,4,1,2 }, new int[] { 0,3,4,2,1 },
             new int[] { 0,4,1,2,3 }, new int[] { 0,4,1,3,2 }, new int[] { 0,4,2,1,3 },
             new int[] { 0,4,2,3,1 }, new int[] { 0,4,3,1,2 }, new int[] { 0,4,3,2,1 },
             new int[] { 1,0,2,3,4 }, new int[] { 1,0,2,4,3 }, new int[] { 1,0,3,2,4 }, 
             new int[] { 1,0,3,4,2 }, new int[] { 1,0,4,2,3 }, new int[] { 1,0,4,3,2 },
             new int[] { 1,2,0,3,4 }, new int[] { 1,2,0,4,3 }, new int[] { 1,2,3,0,4 },
             new int[] { 1,2,3,4,0 }, new int[] { 1,2,4,0,3 }, new int[] { 1,2,4,3,0 },
             new int[] { 1,3,0,2,4 }, new int[] { 1,3,0,4,2 }, new int[] { 1,3,2,0,4 },
             new int[] { 1,3,2,4,0 }, new int[] { 1,3,4,0,2 }, new int[] { 1,3,4,2,0 },
             new int[] { 1,4,0,2,3 }, new int[] { 1,4,0,3,2 }, new int[] { 1,4,2,0,3 },
             new int[] { 1,4,2,3,0 }, new int[] { 1,4,3,0,2 }, new int[] { 1,4,3,2,0 },
             new int[] { 2,0,1,3,4 }, new int[] { 2,0,1,4,3 }, new int[] { 2,0,3,1,4 },
             new int[] { 2,0,3,4,1 }, new int[] { 2,0,4,1,3 }, new int[] { 2,0,4,3,1 },
             new int[] { 2,1,0,3,4 }, new int[] { 2,1,0,4,3 }, new int[] { 2,1,3,0,4 },
             new int[] { 2,1,3,4,0 }, new int[] { 2,1,4,0,3 }, new int[] { 2,1,4,3,0 },
             new int[] { 2,3,0,1,4 }, new int[] { 2,3,0,4,1 }, new int[] { 2,3,1,0,4 },
             new int[] { 2,3,1,4,0 }, new int[] { 2,3,4,0,1 }, new int[] { 2,3,4,1,0 },
             new int[] { 2,4,0,1,3 }, new int[] { 2,4,0,3,1 }, new int[] { 2,4,1,0,3 },
             new int[] { 2,4,1,3,0 }, new int[] { 2,4,3,0,1 }, new int[] { 2,4,3,1,0 },
             new int[] { 3,0,1,2,4 }, new int[] { 3,0,1,4,2 }, new int[] { 3,0,2,1,4 },
             new int[] { 3,0,2,4,1 }, new int[] { 3,0,4,1,2 }, new int[] { 3,0,4,2,1 },
             new int[] { 3,1,0,2,4 }, new int[] { 3,1,0,4,2 }, new int[] { 3,1,2,0,4 },
             new int[] { 3,1,2,4,0 }, new int[] { 3,1,4,0,2 }, new int[] { 3,1,4,2,0 },
             new int[] { 3,2,0,1,4 }, new int[] { 3,2,0,4,1 }, new int[] { 3,2,1,0,4 },
             new int[] { 3,2,1,4,0 }, new int[] { 3,2,4,0,1 }, new int[] { 3,2,4,1,0 },
             new int[] { 3,4,0,1,2 }, new int[] { 3,4,0,2,1 }, new int[] { 3,4,1,0,2 },
             new int[] { 3,4,1,2,0 }, new int[] { 3,4,2,0,1 }, new int[] { 3,4,2,1,0 },
             new int[] { 4,0,1,2,3 }, new int[] { 4,0,1,3,2 }, new int[] { 4,0,2,1,3 },
             new int[] { 4,0,2,3,1 }, new int[] { 4,0,3,1,2 }, new int[] { 4,0,3,2,1 },
             new int[] { 4,1,0,2,3 }, new int[] { 4,1,0,3,2 }, new int[] { 4,1,2,0,3 },
             new int[] { 4,1,2,3,0 }, new int[] { 4,1,3,0,2 }, new int[] { 4,1,3,2,0 },
             new int[] { 4,2,0,1,3 }, new int[] { 4,2,0,3,1 }, new int[] { 4,2,1,0,3 },
             new int[] { 4,2,1,3,0 }, new int[] { 4,2,3,0,1 }, new int[] { 4,2,3,1,0 },
             new int[] { 4,3,0,1,2 }, new int[] { 4,3,0,2,1 },
             new int[] { 4,3,1,0,2 }, new int[] { 4,3,1,2,0 },
             new int[] { 4,3,2,0,1 }, new int[] { 4,3,2,1,0 }
        };
    }

    public void Solve_BruteForce_01()
    {
        // iterate cigarettes
        for (int i = 0; i < this.five_permutations.Length; i++)
        {
            int[] perm1 = this.five_permutations[i];
            this.houses[perm1[0]].Cigarette = Cigarette.PallMall;
            this.houses[perm1[1]].Cigarette = Cigarette.Marlboro;
            this.houses[perm1[2]].Cigarette = Cigarette.DunHill;
            this.houses[perm1[3]].Cigarette = Cigarette.Winfield;
            this.houses[perm1[4]].Cigarette = Cigarette.Rothmans;

            // iterate pets
            for (int j = 0; j < this.five_permutations.Length; j++)
            {
                int[] perm2 = this.five_permutations[j];
                this.houses[perm2[0]].Pet = Pet.Bird;
                this.houses[perm2[1]].Pet = Pet.Cat;
                this.houses[perm2[2]].Pet = Pet.Dog;
                this.houses[perm2[3]].Pet = Pet.Fish;
                this.houses[perm2[4]].Pet = Pet.Horse;

                // iterate drinks
                for (int k = 0; k < this.four_permutations.Length; k++)
                {
                    char[] perm3 = this.four_permutations[k];

                    int k1 =
                        (perm3[0] == 'a') ? 0 : (perm3[0] == 'b') ? 1 :
                        (perm3[0] == 'c') ? 3 : 4;
                    int k2 =
                        (perm3[1] == 'a') ? 0 : (perm3[1] == 'b') ? 1 :
                        (perm3[1] == 'c') ? 3 : 4;
                    int k3 =
                        (perm3[2] == 'a') ? 0 : (perm3[2] == 'b') ? 1 :
                        (perm3[2] == 'c') ? 3 : 4;
                    int k4 =
                        (perm3[3] == 'a') ? 0 : (perm3[3] == 'b') ? 1 :
                        (perm3[3] == 'c') ? 3 : 4;

                    this.houses[k1].Drink = Drink.Beer;
                    this.houses[k2].Drink = Drink.Coffee;
                    this.houses[k3].Drink = Drink.Tea;
                    this.houses[k4].Drink = Drink.Water;

                    // iterate colors
                    for (int l = 0; l < this.four_permutations.Length; l++)
                    {
                        char[] perm4 = this.four_permutations[l];

                        int l1 =
                            (perm4[0] == 'a') ? 0 : (perm4[0] == 'b') ? 2 :
                            (perm4[0] == 'c') ? 3 : 4;
                        int l2 =
                            (perm4[1] == 'a') ? 0 : (perm4[1] == 'b') ? 2 :
                            (perm4[1] == 'c') ? 3 : 4;
                        int l3 =
                            (perm4[2] == 'a') ? 0 : (perm4[2] == 'b') ? 2 :
                            (perm4[2] == 'c') ? 3 : 4;
                        int l4 =
                            (perm4[3] == 'a') ? 0 : (perm4[3] == 'b') ? 2 :
                            (perm4[3] == 'c') ? 3 : 4;

                        this.houses[l1].HouseColor = HouseColor.Green;
                        this.houses[l2].HouseColor = HouseColor.Red;
                        this.houses[l3].HouseColor = HouseColor.White;
                        this.houses[l4].HouseColor = HouseColor.Yellow;

                        // iterate nations
                        for (int m = 0; m < this.four_permutations.Length; m++)
                        {
                            char[] perm5 = this.four_permutations[m];

                            int m1 =
                                (perm5[0] == 'a') ? 1 : (perm5[0] == 'b') ? 2 :
                                (perm5[0] == 'c') ? 3 : 4;
                            int m2 =
                                (perm5[1] == 'a') ? 1 : (perm5[1] == 'b') ? 2 :
                                (perm5[1] == 'c') ? 3 : 4;
                            int m3 =
                                (perm5[2] == 'a') ? 1 : (perm5[2] == 'b') ? 2 :
                                (perm5[2] == 'c') ? 3 : 4;
                            int m4 =
                                (perm5[3] == 'a') ? 1 : (perm5[3] == 'b') ? 2 :
                                (perm5[3] == 'c') ? 3 : 4;

                            this.houses[m1].Nationality = Nationality.British;
                            this.houses[m2].Nationality = Nationality.Swedish;
                            this.houses[m3].Nationality = Nationality.Danish;
                            this.houses[m4].Nationality = Nationality.German;

                            // Tipp 1: "Der Brite lebt im roten Haus"
                            if (!this.Tip_01_Verify())
                                continue;

                            // Tipp 2: "Der Schwede hält einen Hund"
                            if (!this.Tip_02_Verify())
                                continue;

                            // Tipp 3: "Der Däne trinkt gerne Tee"
                            if (!this.Tip_03_Verify())
                                continue;

                            // Tipp 4: "Das grüne Haus steht links vom weißen Haus"
                            if (!this.Tip_04_Verify())
                                continue;

                            // Tipp 5: "Der Besitzer des grünen Hauses trinkt Kaffee"
                            if (!this.Tip_05_Verify())
                                continue;

                            // Tipp 6: "Die Person, die Pall Mall raucht, hält einen Vogel"
                            if (!this.Tip_06_Verify())
                                continue;

                            // Tipp 7: "Der Mann, der im mittleren Haus wohnt, trinkt gerne Milch"
                            // See C'tor

                            // Tipp 8: "Der Besitzer des gelben Hauses raucht Dunhill"
                            if (!this.Tip_08_Verify())
                                continue;

                            // Tipp 9: "Der Norweger wohnt im ersten Haus"
                            // See C'tor

                            // Tipp 10: "Der Marlboro-Raucher wohnt neben dem, der eine Katze hält. "
                            if (!this.Tip_10_Verify())
                                continue;

                            // Tipp 11: "Der Mann, der ein Pferd hält, wohnt neben dem, der Dunhill raucht"
                            if (!this.Tip_11_Verify())
                                continue;

                            // Tipp 12: "Der Winfield-Raucher trinkt gerne Bier."
                            if (!this.Tip_12_Verify())
                                continue;

                            // Tipp 13: "Neben dem blauen Haus wohnt der Norweger"
                            // See C'tor

                            // Tipp 14: "Der Deutsche raucht Rothmanns"
                            if (!this.Tip_14_Verify())
                                continue;

                            // Tipp 15: "Der Marlboro-Raucher hat einen Nachbarn, der Wasser trinkt"
                            if (!this.Tip_15_Verify())
                                continue;

                            // print solution
                            this.PrintSolution();
                        }
                    }
                }
            }
        }
    }

    public void Solve_BruteForce_02()
    {
        // iterate cigarettes
        for (int i = 0; i < this.five_permutations.Length; i++)
        {
            int[] perm1 = this.five_permutations[i];
            this.houses[perm1[0]].Cigarette = Cigarette.PallMall;
            this.houses[perm1[1]].Cigarette = Cigarette.Marlboro;
            this.houses[perm1[2]].Cigarette = Cigarette.DunHill;
            this.houses[perm1[3]].Cigarette = Cigarette.Winfield;
            this.houses[perm1[4]].Cigarette = Cigarette.Rothmans;

            // iterate pets
            for (int j = 0; j < this.five_permutations.Length; j++)
            {
                int[] perm2 = this.five_permutations[j];
                this.houses[perm2[0]].Pet = Pet.Bird;
                this.houses[perm2[1]].Pet = Pet.Cat;
                this.houses[perm2[2]].Pet = Pet.Dog;
                this.houses[perm2[3]].Pet = Pet.Fish;
                this.houses[perm2[4]].Pet = Pet.Horse;

                // Tipp 6: "Die Person, die Pall Mall raucht, hält einen Vogel"
                if (!this.Tip_06_Verify())
                    continue;

                // Tipp 10: "Der Marlboro-Raucher wohnt neben dem, der eine Katze hält. "
                if (!this.Tip_10_Verify())
                    continue;

                // Tipp 11: "Der Mann, der ein Pferd hält, wohnt neben dem, der Dunhill raucht"
                if (!this.Tip_11_Verify())
                    continue;

                // iterate drinks
                for (int k = 0; k < this.four_permutations.Length; k++)
                {
                    char[] perm3 = this.four_permutations[k];

                    int k1 =
                        (perm3[0] == 'a') ? 0 : (perm3[0] == 'b') ? 1 :
                        (perm3[0] == 'c') ? 3 : 4;
                    int k2 =
                        (perm3[1] == 'a') ? 0 : (perm3[1] == 'b') ? 1 :
                        (perm3[1] == 'c') ? 3 : 4;
                    int k3 =
                        (perm3[2] == 'a') ? 0 : (perm3[2] == 'b') ? 1 :
                        (perm3[2] == 'c') ? 3 : 4;
                    int k4 =
                        (perm3[3] == 'a') ? 0 : (perm3[3] == 'b') ? 1 :
                        (perm3[3] == 'c') ? 3 : 4;

                    this.houses[k1].Drink = Drink.Beer;
                    this.houses[k2].Drink = Drink.Coffee;
                    this.houses[k3].Drink = Drink.Tea;
                    this.houses[k4].Drink = Drink.Water;

                    // Tipp 12: "Der Winfield-Raucher trinkt gerne Bier."
                    if (!this.Tip_12_Verify())
                        continue;

                    // Tipp 15: "Der Marlboro-Raucher hat einen Nachbarn, der Wasser trinkt"
                    if (!this.Tip_15_Verify())
                        continue;

                    // iterate colors
                    for (int l = 0; l < this.four_permutations.Length; l++)
                    {
                        char[] perm4 = this.four_permutations[l];

                        int l1 =
                            (perm4[0] == 'a') ? 0 : (perm4[0] == 'b') ? 2 :
                            (perm4[0] == 'c') ? 3 : 4;
                        int l2 =
                            (perm4[1] == 'a') ? 0 : (perm4[1] == 'b') ? 2 :
                            (perm4[1] == 'c') ? 3 : 4;
                        int l3 =
                            (perm4[2] == 'a') ? 0 : (perm4[2] == 'b') ? 2 :
                            (perm4[2] == 'c') ? 3 : 4;
                        int l4 =
                            (perm4[3] == 'a') ? 0 : (perm4[3] == 'b') ? 2 :
                            (perm4[3] == 'c') ? 3 : 4;

                        this.houses[l1].HouseColor = HouseColor.Green;
                        this.houses[l2].HouseColor = HouseColor.Red;
                        this.houses[l3].HouseColor = HouseColor.White;
                        this.houses[l4].HouseColor = HouseColor.Yellow;

                        // Tipp 4: "Das grüne Haus steht links vom weißen Haus"
                        if (!this.Tip_04_Verify())
                            continue;

                        // Tipp 5: "Der Besitzer des grünen Hauses trinkt Kaffee"
                        if (!this.Tip_05_Verify())
                            continue;

                        // Tipp 8: "Der Besitzer des gelben Hauses raucht Dunhill"
                        if (!this.Tip_08_Verify())
                            continue;

                        // iterate nations
                        for (int m = 0; m < this.four_permutations.Length; m++)
                        {
                            char[] perm5 = this.four_permutations[m];

                            int m1 =
                                (perm5[0] == 'a') ? 1 : (perm5[0] == 'b') ? 2 :
                                (perm5[0] == 'c') ? 3 : 4;
                            int m2 =
                                (perm5[1] == 'a') ? 1 : (perm5[1] == 'b') ? 2 :
                                (perm5[1] == 'c') ? 3 : 4;
                            int m3 =
                                (perm5[2] == 'a') ? 1 : (perm5[2] == 'b') ? 2 :
                                (perm5[2] == 'c') ? 3 : 4;
                            int m4 =
                                (perm5[3] == 'a') ? 1 : (perm5[3] == 'b') ? 2 :
                                (perm5[3] == 'c') ? 3 : 4;

                            this.houses[m1].Nationality = Nationality.British;
                            this.houses[m2].Nationality = Nationality.Swedish;
                            this.houses[m3].Nationality = Nationality.Danish;
                            this.houses[m4].Nationality = Nationality.German;

                            // Tipp 1: "Der Brite lebt im roten Haus"
                            if (!this.Tip_01_Verify())
                                continue;

                            // Tipp 2: "Der Schwede hält einen Hund"
                            if (!this.Tip_02_Verify())
                                continue;

                            // Tipp 3: "Der Däne trinkt gerne Tee"
                            if (!this.Tip_03_Verify())
                                continue;

                            // Tipp 14: "Der Deutsche raucht Rothmanns"
                            if (!this.Tip_14_Verify())
                                continue;

                            // print solution
                            this.PrintSolution();
                        }
                    }
                }
            }
        }
    }

    // Tipp 1: "Der Brite lebt im roten Haus"
    private bool Tip_01_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].Nationality == Nationality.British)
            {
                if (this.houses[i].HouseColor == HouseColor.Red)
                {
                    tip = true;
                }
                break;
            }
        }
        return tip;
    }

    // Tipp 2: "Der Schwede hält einen Hund"
    private bool Tip_02_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].Nationality == Nationality.Swedish)
            {
                if (this.houses[i].Pet == Pet.Dog)
                {
                    tip = true;
                } 
                break;
            }
        }
        return tip;
    }

    // Tipp 3: "Der Däne trinkt gerne Tee"
    private bool Tip_03_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].Nationality == Nationality.Danish)
            {
                if (this.houses[i].Drink == Drink.Tea)
                {
                    tip = true;
                }
                break;
            }
        }
        return tip;
    }

    // Tipp 4: "Das grüne Haus steht direkt links vom weißen Haus"
    private bool Tip_04_Verify()
    {
        if ((this.houses[2].HouseColor == HouseColor.Green &&
                this.houses[3].HouseColor == HouseColor.White) ||
            (this.houses[3].HouseColor == HouseColor.Green &&
                this.houses[4].HouseColor == HouseColor.White))
        {
            return true;
        }
        else
            return false;
    }

    // Tipp 5: "Der Besitzer des grünen Hauses trinkt gerne Kaffee"
    private bool Tip_05_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].HouseColor == HouseColor.Green)
            {
                if (this.houses[i].Drink == Drink.Coffee)
                {
                    tip = true;
                }
                break;
            }
        }
        return tip;
    }

    // Tipp 6: "Die Person, die Pall Mall raucht, hält einen Vogel"
    private bool Tip_06_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].Cigarette == Cigarette.PallMall)
            {
                if (this.houses[i].Pet == Pet.Bird)
                {
                    tip = true;
                }
                break;
            }
        }
        return tip;
    }

    // Tipp 7: "Der Mann, der im mittleren Haus wohnt, trinkt gerne Milch"
    // See C'tor

    // Tipp 8: "Der Besitzer des gelben Hauses raucht Dunhill"
    private bool Tip_08_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].HouseColor == HouseColor.Yellow)
            {
                if (this.houses[i].Cigarette == Cigarette.DunHill)
                {
                    tip = true;
                }
                break;
            }
        }
        return tip;
    }

    // Tipp 9: "Der Norweger wohnt im ersten Haus"
    // See C'tor

    // Tipp 10: "Der Marlboro-Raucher wohnt neben dem, der eine Katze hält"
    private bool Tip_10_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].Cigarette == Cigarette.Marlboro)
            {
                if (i == 0)
                {
                    if (this.houses[1].Pet == Pet.Cat)
                        tip = true;
                }
                else if (i == 4)
                {
                    if (this.houses[3].Pet == Pet.Cat)
                        tip = true;
                }
                else if (this.houses[i - 1].Pet == Pet.Cat ||
                    this.houses[i + 1].Pet == Pet.Cat)
                    tip = true;

                break;
            }
        }

        return tip;
    }

    // Tipp 11: "Der Mann, der ein Pferd hält, wohnt neben dem, der Dunhill raucht"
    private bool Tip_11_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].Pet == Pet.Horse)
            {
                if (i == 0)
                {
                    if (this.houses[1].Cigarette == Cigarette.DunHill)
                        tip = true;
                }
                else if (i == 4)
                {
                    if (this.houses[3].Cigarette == Cigarette.DunHill)
                        tip = true;
                }
                else if (this.houses[i - 1].Cigarette == Cigarette.DunHill ||
                            this.houses[i + 1].Cigarette == Cigarette.DunHill)
                        tip = true;
            
                break;
            }
        }
        return tip;
    }

    // Tipp 12: "Der Winfield-Raucher trinkt gerne Bier"
    private bool Tip_12_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].Cigarette == Cigarette.Winfield)
            {
                if (this.houses[i].Drink == Drink.Beer)
                    tip = true;

                break;
            }
        }
        return tip;
    }

    // Tipp 13: "Neben dem blauen Haus wohnt der Norweger"
    // See C'tor

    // Tipp 14: "Der Deutsche raucht Rothmanns"
    private bool Tip_14_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].Nationality == Nationality.German)
            {
                if (this.houses[i].Cigarette == Cigarette.Rothmans)
                    tip = true;

                break;
            }
        }
        return tip;
    }

    // Tipp 15: "Der Marlboro-Raucher hat einen Nachbarn, der Wasser trinkt"
    private bool Tip_15_Verify()
    {
        bool tip = false;
        for (int i = 0; i < 5; i++)
        {
            if (this.houses[i].Cigarette == Cigarette.Marlboro)
            {
                if (i == 0)
                {
                    if (this.houses[1].Drink == Drink.Water)
                        tip = true;
                }
                else if (i == 4)
                {
                    if (this.houses[3].Drink == Drink.Water)
                        tip = true;
                }
                else if (this.houses[i - 1].Drink == Drink.Water ||
                    this.houses[i + 1].Drink == Drink.Water)
                        tip = true;

                break;
            }
        }
        return tip;
    }

    public void PrintSolution()
    {
        for (int n = 0; n < 5; n++)
            if (this.houses[n].Pet == Pet.Fish)
                Console.WriteLine("The {0} is the owner of the Fish.",
                    this.houses[n].Nationality);
    }
}

