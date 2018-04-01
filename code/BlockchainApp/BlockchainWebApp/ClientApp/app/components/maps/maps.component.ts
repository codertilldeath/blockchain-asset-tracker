import { Component, AfterViewInit, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { TransactionService } from '../transaction/transaction.service';
import { google, GoogleMap } from '@agm/core/services/google-maps-types';
import { LatLngBounds } from '@agm/core';
import { Observable } from 'rxjs/Observable';
import { ITransaction } from '../transaction/transaction';

@Component({
	selector: 'maps',
	templateUrl: './maps.component.html',
	styleUrls: ['./maps.component.css'], 
	providers: [TransactionService]
})
export class MapsComponent implements OnInit {
	zoom: number = 2;
	lat: number;
    lng: number;
    markers: marker[];
    
    searchLocations(PropertyID: string) { }

    async ngOnInit() {
        //this.updateMap(await this.mapsService.getTransaction());
    }

    async searchHistory(propId: string) {
        this.updateMap(await this.mapsService.getPropertyHistory(propId));
    }

    updateMap(transactions: ITransaction[]) {
        this.markers = transactions;
        this.lat = this.markers[0].latitude;
        this.lng = this.markers[0].longitude;
        this.zoom = 15;
    }

    constructor(private mapsService: TransactionService) {
    }
}

interface marker {

	latitude: number;
	longitude: number;
}
