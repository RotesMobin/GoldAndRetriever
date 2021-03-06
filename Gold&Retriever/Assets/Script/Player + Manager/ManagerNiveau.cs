﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNiveau : MonoBehaviour {

    // chunk milieu = 40 largeur , 20 hauteur 
    // chunk Coin = 20 largeur , 40 hauteur 
    private int HAUTEURMAX = -20;
    private int LARGEURMAX = 40;

    // Tableau de prefable annexe de map : 
    public List<GameObject> TabAnnexe;

    // Tableau de prefable Milieu de map : 
    public List<GameObject> TabMilieu;
    private List<bool> TabMilieuBool;

    private List<bool> TabCoinBool;
    // Tableau de prefable coin de map : 
    public List<GameObject> TabCoin;

    public List<GameObject> TabSpecialHaut;

    // Tableau de prefable coin de map : 
    public GameObject TabDeb;
    public GameObject TabDebGrotte;

    public GameObject TabFin;

    public GameObject laveGame; // un seul à gerer le parent de tout les prefabLave
    public GameObject prefabLave;

    // special chunk 1 par map : 
    private bool boolSpecialHaut;
    private int ChanceChunkSpecial = 2; 

    // Use this for initialization
    void Start () {
        TabMilieuBool = new List<bool>();
        TabCoinBool = new List<bool>(); 
        initableauBoolTrue(ref TabMilieuBool,TabMilieu.Count);
        initableauBoolTrue(ref TabCoinBool, TabCoin.Count); 
        boolSpecialHaut = false; 

        int maxLigne = 4;
        int maxColonne = 4;

        int rLigne = Random.Range(2, maxLigne);
        int rColonne = Random.Range(2, maxColonne);

        int sens = 1; // -1 = inverser 

        int indiceLigne = 40;
        int indiceColonne = 1;

        int pourcentageChanceSpecialChunk = 20; 

        InstanciateGameObjRandom(TabDebGrotte , new Vector3(0,1,0) , 1 , 0);
        
        for (int i = 1;  i <=  rColonne; i++)
        {
            rLigne = Random.Range(2, maxLigne);
            if (sens == 1)
            {
                // ligne 
                for (int j = 1; j <= rLigne; j++)
                {
                    if(i == 1 && Random.Range(0, ChanceChunkSpecial) == 0 && !boolSpecialHaut)
                    {
                        int r = Random.Range(0, TabSpecialHaut.Count);
                        InstanciateGameObjRandom(TabSpecialHaut[r], new Vector3(indiceLigne, indiceColonne, 0), sens, 0);
                        indiceLigne = indiceLigne + 40;
                        boolSpecialHaut = true; 
                    }
                    else
                    {
                        //int r = Random.Range(0, TabMilieu.Count);
                        int r = renvoiUnRandomOk(ref TabMilieuBool, TabMilieu.Count);
                        InstanciateGameObjRandom(TabMilieu[r], new Vector3(indiceLigne, indiceColonne, 0), sens, 0);
                        indiceLigne = indiceLigne + 40;
                    }
                  
                }

                //int rCoin = Random.Range(0, TabCoin.Count);
                int rCoin = renvoiUnRandomOk(ref TabCoinBool, TabCoin.Count);
                
                InstanciateGameObjRandom(TabCoin[rCoin], new Vector3(indiceLigne, indiceColonne, 0), -sens, 1);

                // Chunk Special : 
                chunkAnnexe(indiceLigne,indiceColonne,sens,pourcentageChanceSpecialChunk,0); 


            }
            else
            {
               // indiceLigne = indiceLigne;
                // ligne Autre sens
                for (int j = 1; j <= rLigne; j++)
                {
                    indiceLigne = indiceLigne - 40;
                    //int r = Random.Range(0, TabMilieu.Count);
                    int r = renvoiUnRandomOk(ref TabMilieuBool, TabMilieu.Count);
                    InstanciateGameObjRandom(TabMilieu[r], new Vector3(indiceLigne, indiceColonne, 0), sens, 0);
                    
                }

                int rCoin = renvoiUnRandomOk(ref TabCoinBool, TabCoin.Count);
                InstanciateGameObjRandom(TabCoin[rCoin], new Vector3(indiceLigne-20, indiceColonne, 0), -sens, 1);

                // Chunk Special : 
                chunkAnnexe(indiceLigne, indiceColonne, sens, pourcentageChanceSpecialChunk,1);

            }
            sens = sens == 1 ? -1 : 1;
            indiceColonne = indiceColonne - 21;
        }
        
        InstanciateGameObjRandom(TabFin, new Vector3(indiceLigne , indiceColonne, 0), -sens, 2);


        indiceColonne = indiceColonne - 1; 
        int posXLave = -20;
        laveGame.transform.position = new Vector3(posXLave, indiceColonne); 
        for (int i= 0; i < rColonne + 4; i++)
        {
            LaveSpawn(new Vector3(posXLave, indiceColonne ), 0);
            posXLave += 40;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	}

    void InstanciateGameObjRandom(GameObject Go , Vector3 position, int direction, int milCoinFin)
    {
        GameObject gameobjTemp = Instantiate(Go);

        Vector3 temp = gameobjTemp.transform.localScale;
        temp.x = direction;


        if (direction == -1 && milCoinFin == 0)
            position.x += 40;

        if (direction == -1 && milCoinFin == 1)
            position.x += 20;

        if (direction == -1 && milCoinFin == 2)
            position.x += 40;   
        if (direction == 1 && milCoinFin == 2)
            position.x -= 40;


        gameobjTemp.transform.localScale = temp;
        gameobjTemp.transform.position = position;
    }

    void LaveSpawn(Vector3 position, int timeLave)
    {
        GameObject gameobjTemp = Instantiate(prefabLave);
        gameobjTemp.transform.parent = laveGame.transform; 
        gameobjTemp.transform.position = position;

    }

    // Chunk Annexe 
    void chunkAnnexe(float indiceLigne , float indiceColonne, int sens, int pourcentageChanceSpecialChunk, int sensAdaptation)
    {
        float modifLine = 1; 
        int chanceSpecialChunk = Random.Range(0, 100);
        if (sens == -1)
            modifLine = -2;

        if (chanceSpecialChunk < pourcentageChanceSpecialChunk)
        {
            int specialeChunk = Random.Range(1, TabAnnexe.Count);
            InstanciateGameObjRandom(TabAnnexe[specialeChunk], new Vector3(indiceLigne + 20 * modifLine, indiceColonne, 0), sens, sensAdaptation);
            InstanciateGameObjRandom(TabAnnexe[0], new Vector3(indiceLigne + 20 * modifLine, indiceColonne - 21, 0), sens, sensAdaptation);
        }
        else
        {
            InstanciateGameObjRandom(TabAnnexe[0], new Vector3(indiceLigne + 20 * modifLine, indiceColonne, 0), sens, sensAdaptation);

            chanceSpecialChunk = Random.Range(0, 100);
            if (chanceSpecialChunk < pourcentageChanceSpecialChunk)
            {
                int specialeChunk = Random.Range(1, TabAnnexe.Count);
                InstanciateGameObjRandom(TabAnnexe[specialeChunk], new Vector3(indiceLigne + 20 * modifLine, indiceColonne - 21, 0), sens, sensAdaptation);
            }
            else
            {
                InstanciateGameObjRandom(TabAnnexe[0], new Vector3(indiceLigne + 20 * modifLine, indiceColonne - 21, 0), sens, sensAdaptation);
                chanceSpecialChunk = Random.Range(0, 100);
            }
        }
    }

    void initableauBoolTrue(ref List<bool> tab,int taille)
    {
        for (int i= 0; i < taille ; i++)
            tab.Add(false);
    }

    void tableauBoolTrue(ref List<bool> tab)
    {
        for (int i = 0; i < tab.Count; i++)
            tab[i] = false ; 
    }


    int renvoiUnRandomOk(ref List<bool> tab,int taille)
    {
       
        if (toutEstUseTab(ref tab))
            tableauBoolTrue(ref tab);

        int RandInt = Random.Range(0, taille);

        int fautPasAbuser = 0; 
        while (fautPasAbuser < 100)
        {
            if (tab[RandInt] == false)
            {
                tab[RandInt] = true;
                //afficheTab(tab);
                return RandInt;
            }
            RandInt = Random.Range(0, taille);
            fautPasAbuser++;
        }
        tableauBoolTrue(ref tab); 


        return RandInt;
    }

    // renvoi vrai si tout est use 
    bool toutEstUseTab(ref List<bool> tab)
    {
        for(int i= 0; i <tab.Count; i++)
        {
            if (tab[i] == false) // pas use
                return false; 
        }
        return true; 
    }

    void afficheTab(List<bool> tab)
    {
        for (int i = 0; i < tab.Count; i++)
            Debug.Log(" i : " + i + " tab : " + tab[i].ToString());
    }
}
