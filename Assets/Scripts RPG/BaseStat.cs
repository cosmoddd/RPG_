using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseStat {

    public List <StatBonus> StatModifiers { get; set; }
    public int BaseValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public int FinalValue { get; set; }

    public BaseStat(int baseValue, string statName, string statDescription)
        {
        this.StatModifiers = new List<StatBonus>();
        this.BaseValue = baseValue;
        this.StatName = statName;
        this.StatDescription = StatDescription;
        }

    public void AddStatBonus(StatBonus statBonus)
        {
           this.StatModifiers.Add(statBonus);
        }

    public void RemoveStatBonus(StatBonus statBonus)
        {
            this.StatModifiers.Remove(StatModifiers.Find(x=> x.BonusValue == statBonus.BonusValue));
        }

    public int GetCalculatedStatValue()
    {
        this.FinalValue = 0;
        this.StatModifiers.ForEach(x => this.FinalValue += x.BonusValue);
        FinalValue += BaseValue;

        return FinalValue;
    }

}

