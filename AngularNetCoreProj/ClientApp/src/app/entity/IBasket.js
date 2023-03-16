"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.IBasketTotal = exports.IBasket = void 0;
var IBasket = /** @class */ (function () {
    function IBasket() {
        this.items = [];
        this.subtotal = 0;
        this.shipment = 0;
        this.total = 0;
    }
    return IBasket;
}());
exports.IBasket = IBasket;
var IBasketTotal = /** @class */ (function () {
    function IBasketTotal() {
        this.total = 0;
    }
    return IBasketTotal;
}());
exports.IBasketTotal = IBasketTotal;
//# sourceMappingURL=IBasket.js.map