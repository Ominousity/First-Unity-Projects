import 'package:flutter/material.dart';

import 'create_channel_dialog.dart';

class CreateChannelButton extends StatelessWidget {
  const CreateChannelButton({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return IconButton(
      onPressed: () {
        showDialog(
          context: context,
          barrierDismissible: false, // user must tap button!
          builder: (BuildContext context) {
            return const CreateChannelDialog();
          },
        );
      },
      icon: const Icon(Icons.group_add),
    );
  }
}
