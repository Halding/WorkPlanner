import 'package:flutter/material.dart';

class LoginScreenTopImage extends StatelessWidget {
  const LoginScreenTopImage({
    Key? key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Column(
      children: const [
        Text(
          "LOGIN",
          style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: 25,
              color: Color(0xFF2196F3)),
        ),
        Padding(padding: EdgeInsets.fromLTRB(0, 0, 0, 75)),
      ],
    );
  }
}
