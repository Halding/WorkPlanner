import 'package:flutter/services.dart';
import 'dart:developer';
import '../models/post.dart';
import 'package:http/http.dart' as http;

import '../models/shift.dart';

class RemoteService {
  var client = http.Client();

  Future<List<Post>?> getPosts() async {
    var uri = Uri.parse('https://jsonplaceholder.typicode.com/posts');
    var response = await client.get(uri);

    if (response.statusCode == 200) {
      var json = response.body;
      return postFromJson(json);
    }
    return null;
  }

  Future<List<Shift>?> getShift() async {
    var uri = Uri.parse('http://10.0.2.2/:7293/api/shift');
    var response = await client.get(uri);
    log(response.statusCode.toString());
    if (response.statusCode == 200) {
      var json = response.body;
      return shiftFromJson(json);
    }
    return null;
  }

  Future<List<Shift>?> readShiftJson() async {
    final String response =
        await rootBundle.loadString('assets/sample.json');
    return shiftFromJson(response);
  }
}
