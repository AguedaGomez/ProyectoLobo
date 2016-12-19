﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LocationDiscovery : MonoBehaviour {


    private List<string> nextToMeLocations;
    private bool visited = false;


    // Use this for initialization
    void Start () {
        nextToMeLocations = new List<string>();
        FillNextoMeLocations();
        
	}



    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D IACollider)
    {
        if (IACollider.gameObject.tag == "IA" && IACollider.gameObject.name == "IA0") {
            //Debug.Log("Collider de: " + this.name);
            IACollider.gameObject.GetComponent<RoomMemory>().AddLocation(this.name);
        }
    }


    internal List<string> TraceRoute(string current, string destination)
    {
        List<string> route = new List<string>();

        if (this.name == destination)
        {
            route.Add(this.name);
        }
        else
        {
            foreach (string location in nextToMeLocations)
            {
                TraceRoute(location, destination, current);
            }
        }

        return route;

    }

    private void TraceRoute(string location, string destination, string previousStep)
    {
        throw new NotImplementedException();
    }

    private void FillNextoMeLocations()
    {
        switch (this.name)
        {
            case "Oficina":
                nextToMeLocations.Add("PuertaOficina_Laboratorio");
                nextToMeLocations.Add("PuertaOficina_Pasillo");
                break;
            case "PuertaOficina_Laboratorio":
                nextToMeLocations.Add("Oficina");
                nextToMeLocations.Add("Laboratorio");
                break;
            case "PuertaOficina_Pasillo":
                nextToMeLocations.Add("Oficina");
                nextToMeLocations.Add("PasilloSuperior_Izq");
                break;
            case "PasilloSuperior_Izq":
                nextToMeLocations.Add("PuertaOficina_Pasillo");
                nextToMeLocations.Add("PuertaLaboratorio_Pasillo");
                nextToMeLocations.Add("PuertaSalonCentral_PasilloSuperior");
                nextToMeLocations.Add("PasilloSuperior_Der");
                break;
            case "PuertaLaboratorio_Pasillo":
                nextToMeLocations.Add("PasilloSuperior_Izq");
                nextToMeLocations.Add("Laboratorio");
                break;
            case "PuertaSalonCentral_PasilloSuperior":
                nextToMeLocations.Add("PasilloSuperior_Izq");
                nextToMeLocations.Add("SalonCentral");
                break;
            case "PasilloSuperior_Der":
                nextToMeLocations.Add("PasilloLateral");
                nextToMeLocations.Add("PasilloSuperior_Izq");
                nextToMeLocations.Add("PuertaAlmacen");
                nextToMeLocations.Add("PuertaCocina_Pasillo");
                break;
            case "PuertaCocina_Pasillo":
                nextToMeLocations.Add("PasilloSuperior_Der");
                nextToMeLocations.Add("Cocina");
                break;
            case "PuertaAlmacen":
                nextToMeLocations.Add("PasilloSuperior_Der");
                nextToMeLocations.Add("Almacen");
                break;
            case "Almacen":
                nextToMeLocations.Add("PuertaAlmacen");
                break;
            case "PasilloLateral":
                nextToMeLocations.Add("PasilloSuperior_Der");
                nextToMeLocations.Add("PuertaDorm_Superior");
                nextToMeLocations.Add("PuertaBaño_Inferior");
                nextToMeLocations.Add("PuertaBaño_Superior");
                nextToMeLocations.Add("PuertaDorm_Inferior");
                nextToMeLocations.Add("PasilloInferior_Der");
                break;
            case "PuertaDorm_Superior":
                nextToMeLocations.Add("PasilloLateral");
                nextToMeLocations.Add("DormitorioSuperior");
                break;
            case "DormitorioSuperior":
                nextToMeLocations.Add("PuertaDorm_Superior");
                break;
            case "PuertaBaño_Superior":
                nextToMeLocations.Add("PasilloLateral");
                nextToMeLocations.Add("BañoSuperior");
                break;
            case "BañoSuperior":
                nextToMeLocations.Add("PuertaBaño_Superior");
                break;
            case "PuertaBaño_Inferior":
                nextToMeLocations.Add("PasilloLateral");
                nextToMeLocations.Add("BañoInferior");
                break;
            case "BañoInferior":
                nextToMeLocations.Add("PuertaBaño_Inferior");
                break;
            case "PuertaDorm_Inferior":
                nextToMeLocations.Add("PasilloLateral");
                nextToMeLocations.Add("DormitorioInferior");
                break;
            case "DormitorioInferior":
                nextToMeLocations.Add("PuertaDorm_Inferior");
                break;
            case "PasilloInferior_Der":
                nextToMeLocations.Add("PasilloLateral");
                nextToMeLocations.Add("PuertaBillar_Pasillo");
                nextToMeLocations.Add("PuertaBar_Pasillo");
                nextToMeLocations.Add("PuertaSalonCentral_PasilloInferior");
                nextToMeLocations.Add("PasilloInferior_Izq");
                break;
            case "PuertaBillar_Pasillo":
                nextToMeLocations.Add("PasilloInferior_Der");
                nextToMeLocations.Add("SalaDeBillar");
                break;
            case "SalaDeBillar":
                nextToMeLocations.Add("PuertaBillar_Pasillo");
                nextToMeLocations.Add("PuertaBillar_BarInferior");
                break;
            case "PuertaBillar_BarInferior":
                nextToMeLocations.Add("BarInferior");
                nextToMeLocations.Add("SalaDeBillar");
                break;
            case "PuertaBar_Pasillo":
                nextToMeLocations.Add("PasilloInferior_Der");
                nextToMeLocations.Add("BarInferior");
                break;
            case "BarInferior":
                nextToMeLocations.Add("PuertaBillar_BarInferior");
                nextToMeLocations.Add("PuertaBar_Pasillo");
                break;
            case "PuertaSalonCentral_PasilloInferior":
                nextToMeLocations.Add("PasilloInferior_Der");
                nextToMeLocations.Add("SalonCentral");
                break;
            case "SalonCentral":
                nextToMeLocations.Add("PuertaSalonCentral_PasilloInferior");
                nextToMeLocations.Add("PuertaSalonCentral_PasilloSuperior");
                nextToMeLocations.Add("PuertaSalonCentral_Cocina");
                break;
            case "PuertaSalonCentral_Cocina":
                nextToMeLocations.Add("Cocina");
                nextToMeLocations.Add("SalonCentral");
                break;
            case "Cocina":
                nextToMeLocations.Add("PuertaCocina_Pasillo");
                nextToMeLocations.Add("PuertaSalonCentral_Cocina");
                break;
            case "PasilloInferior_Izq":
                nextToMeLocations.Add("PasilloInferior_Der");
                nextToMeLocations.Add("PuertaGimnasio");
                break;
            case "PuertaGimnasio":
                nextToMeLocations.Add("PasilloInferior_Izq");
                nextToMeLocations.Add("Gimnasio");
                break;
            case "Gimnasio":
                nextToMeLocations.Add("PuertaGimnasio");
                break;

            default:
                Debug.Log("Esto no debería de imprimirse nunca");
                break;
        }
    }
}
