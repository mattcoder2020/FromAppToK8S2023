"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.productservice = void 0;
var core_1 = require("@angular/core");
var operators_1 = require("rxjs/operators");
//Injectable({ providedIn: "root" })
core_1.Injectable();
var productservice = /** @class */ (function () {
    function productservice(http) {
        this.http = http;
    }
    productservice.prototype.ngOnInit = function () {
        var _this = this;
        this.http.get("http://localhost:5002/api/product")
            .pipe(operators_1.map(function (data) { _this.products = data; return true; }));
    };
    return productservice;
}());
exports.productservice = productservice;
//# sourceMappingURL=productservice.js.map