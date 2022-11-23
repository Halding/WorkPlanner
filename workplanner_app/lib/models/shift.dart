// To parse this JSON data, do
//
//     final shift = shiftFromJson(jsonString);

import 'dart:convert';

List<Shift> shiftFromJson(String str) => List<Shift>.from(json.decode(str).map((x) => Shift.fromJson(x)));

String shiftToJson(List<Shift> data) => json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class Shift {
    Shift({
        required this.id,
        required this.startTime,
        required this.endTime,
        this.clockInTime,
        this.clockOutTime,
        required this.employeeId,
        required this.departmentId,
        this.employeeFirstName,
        this.employeeLastName,
        this.employeeNumber,
    });

    int id;
    DateTime startTime;
    DateTime endTime;
    DateTime? clockInTime;
    DateTime? clockOutTime;
    int employeeId;
    int departmentId;
    String? employeeFirstName;
    String? employeeLastName;
    int? employeeNumber;

    factory Shift.fromJson(Map<String, dynamic> json) => Shift(
        id: json["id"],
        startTime: DateTime.parse(json["startTime"]),
        endTime: DateTime.parse(json["endTime"]),
        clockInTime: json["clockInTime"],
        clockOutTime: json["clockOutTime"],
        employeeId: json["employeeId"],
        departmentId: json["departmentId"],
        employeeFirstName: json["employeeFirstName"],
        employeeLastName: json["employeeLastName"],
        employeeNumber: json["employeeNumber"],
    );

    Map<String, dynamic> toJson() => {
        "id": id,
        "startTime": startTime.toIso8601String(),
        "endTime": endTime.toIso8601String(),
        "clockInTime": clockInTime,
        "clockOutTime": clockOutTime,
        "employeeId": employeeId,
        "departmentId": departmentId,
        "employeeFirstName": employeeFirstName,
        "employeeLastName": employeeLastName,
        "employeeNumber": employeeNumber,
    };
}
