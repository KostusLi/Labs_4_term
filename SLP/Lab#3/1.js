var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g = Object.create((typeof Iterator === "function" ? Iterator : Object).prototype);
    return g.next = verb(0), g["throw"] = verb(1), g["return"] = verb(2), typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (g && (g = 0, op[0] && (_ = 0)), _) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
function checkStock(item) {
    return new Promise(function (resolve, reject) {
        console.log("Проверяем наличие товара...");
        setTimeout(function () {
            var stock = ["bim", "bom", "bam"];
            if (stock.includes(item)) {
                resolve({ item: item, price: 700 });
            }
            else {
                reject(new Error("Товара нет в наличии"));
            }
        }, 1000);
    });
}
function processPayment(order) {
    return new Promise(function (resolve, reject) {
        console.log("Обработка оплаты...");
        setTimeout(function () {
            var userBalance = 1000;
            if (userBalance >= 500) {
                resolve("Красавчик, оплатил");
            }
            else {
                reject(new Error("Недостаточно средств"));
            }
        }, 2000);
    });
}
function deliverOrder() {
    return new Promise(function (resolve) {
        console.log("Доставка заказа...");
        setTimeout(function () {
            resolve("Заказ на месте!");
        }, 1500);
    });
}
checkStock("afroamerican")
    .then(function (order) { return processPayment(order); })
    .then(function (result) {
    console.log(result);
    return deliverOrder();
})
    .then(function (delivery) { return console.log(delivery); })
    .catch(function (error) { return console.error("Ошибка:", error.message); })
    .finally(function () {
    console.log("Спасибо за заказ, приходите еще!");
});
//2
function fetchFast() {
    return new Promise(function (resolve) {
        setTimeout(function () {
            resolve("Молния маквин, кчау");
        }, 500);
    });
}
function fetchSlow() {
    return new Promise(function (resolve) {
        setTimeout(function () {
            resolve("Гром всегда позже молнии");
        }, 2000);
    });
}
Promise.race([fetchFast(), fetchSlow()])
    .then(function (result) { return console.log("Победитель:", result); })
    .catch(function (error) { return console.error(error); });
//3
var promises = [
    Promise.resolve("Успех 1"),
    Promise.reject(new Error("Ошибка 1")),
    Promise.resolve("Успех 2"),
    Promise.reject(new Error("Ошибка 2")),
    Promise.resolve("Успех 3")
];
Promise.allSettled(promises)
    .then(function (results) {
    var successful = results
        .filter(function (r) {
        return r.status === "fulfilled";
    })
        .map(function (r) { return r.value; });
    console.log("Красавчики:", successful);
});
//4
console.log("Начало");
setTimeout(function () { return console.log("Таймаут"); }, 0);
Promise.resolve(undefined)
    .then(function () { return console.log("Промис 1"); })
    .then(function () { return console.log("Промис 2"); });
console.log("Конец");
//5
function getData() {
    return __awaiter(this, void 0, void 0, function () {
        var response, data, err_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 3, , 4]);
                    return [4 /*yield*/, fetch("https://api.example.com/data")];
                case 1:
                    response = _a.sent();
                    if (!response.ok) {
                        throw new Error("Ошибка HTTP: " + response.status);
                    }
                    return [4 /*yield*/, response.json()];
                case 2:
                    data = _a.sent();
                    console.log(data);
                    return [3 /*break*/, 4];
                case 3:
                    err_1 = _a.sent();
                    if (err_1 instanceof Error) {
                        console.error("Ошибка:", err_1.message);
                    }
                    else {
                        console.error("Неизвестная ошибка");
                    }
                    return [3 /*break*/, 4];
                case 4: return [2 /*return*/];
            }
        });
    });
}
//6
function limitRequests(tasks, limit) {
    return __awaiter(this, void 0, void 0, function () {
        function worker() {
            return __awaiter(this, void 0, void 0, function () {
                var currentIndex, _a, _b, err_2;
                return __generator(this, function (_c) {
                    switch (_c.label) {
                        case 0:
                            if (!(index < tasks.length)) return [3 /*break*/, 5];
                            currentIndex = index++;
                            _c.label = 1;
                        case 1:
                            _c.trys.push([1, 3, , 4]);
                            _a = results;
                            _b = currentIndex;
                            return [4 /*yield*/, tasks[currentIndex]()];
                        case 2:
                            _a[_b] = _c.sent();
                            return [3 /*break*/, 4];
                        case 3:
                            err_2 = _c.sent();
                            throw err_2;
                        case 4: return [3 /*break*/, 0];
                        case 5: return [2 /*return*/];
                    }
                });
            });
        }
        var results, index, workers, i;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    results = [];
                    index = 0;
                    workers = [];
                    for (i = 0; i < limit; i++) {
                        workers.push(worker());
                    }
                    return [4 /*yield*/, Promise.all(workers)];
                case 1:
                    _a.sent();
                    return [2 /*return*/, results];
            }
        });
    });
}
function createTask(id) {
    return function () {
        return new Promise(function (resolve) {
            console.log("Запуск задачи", id);
            setTimeout(function () {
                console.log("Завершена задача", id);
                resolve("Результат " + id);
            }, 1000);
        });
    };
}
var tasks = Array.from({ length: 10 }, function (_, i) { return createTask(i + 1); });
limitRequests(tasks, 3)
    .then(function (results) {
    console.log("Все задачи завершены:");
    console.log(results);
})
    .catch(function (error) { return console.error(error); });
