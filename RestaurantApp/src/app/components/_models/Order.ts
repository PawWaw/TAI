import { OrderStation } from "./OrderStation";

export class Order {
    id: number;
    orderStationId: number;
    delivererId: number;
    userId: number;
    username?: string;
    status: string;
    startTime: string;
    endTime: string;
    deliverer?: string;
    orderStation: OrderStation;
}